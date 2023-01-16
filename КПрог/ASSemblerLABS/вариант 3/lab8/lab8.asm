.model small   
.stack 100h
.data  
    noDataStr db "No data!", '$'
    noFileStr db "The file with this name doesn't exist!", '$'
    procStartStr db "File processing has started...", '$'
    amountStr db "The amount of non-empty lines is:", 9h, '$'  
    procFinStr db "File processing has finished.", '$'
	finStr db "Program has been executed .", '$'
	addAmountStr db " additional amount:", 9h, '$' 
    endl db 0Dh, 0Ah, '$'
    
    sucMessage1 db "FOPEN: Success!", '$'
    sucMessage2 db "FCLOSE: Success!", '$'
    sucMessage3 db "CEL: RFF: Success!", '$'  
    sucMessage4 db "CEL: SB: Success!", '$'
    sucMessage5 db "CEL: Success!", '$' 
    sucMessage6 db "PR: Success!", '$'
        
    errMessage1 db "FOPEN: File not found!", '$' 
    errMessage2 db "FOPEN: Path not found!", '$'
    errMessage3 db "FOPEN: Too many files are opened!", '$'
    errMessage4 db "FOPEN: Access denied!", '$'
    errMessage5 db "FOPEN: Wrong access mode!", '$'  
    errMessage6 db "FCLOSE: Wrong descryptor!", '$'
    errMessage7 db "CEL: RFF: error!", '$'  
    errMessage8 db "CEL: SB: error!", '$' 
    errMessage9 db "PR: error!", '$'

    maxCmdLen equ 126
    cmdLen dw ? 
    cmdLine db 126 dup(?)
    
    fileDescr dw ?      
    bufSize equ 50
    buf db bufSize+2 dup('$') 
    
    addAmount dw 0 
    nonEmptyLines dw 0
    chars dw 0    
    cf dw 0 
    eof dw 0
.code    

;MACROSES  

;displays string with carriage transfer
display macro string
    pusha
    displaySingle string
    displaySingle endl
    popa
endm 

;displays only string 
displaySingle macro string
    lea dx, string
    mov ah, 09h
    int 21h
endm 

;calls interrupt, wich ends an
;application 
macro exit
   mov ax,4C00h
   int 21h  
endm   

;END MACROSES
 
;MAIN 
 
main: 
    mov ax, @data
    mov ds, ax    
    call getComArgs    
    
    ;check if cmd is empty
    mov ax, cmdLen    
    cmp cmdLen, 1
    jle noData
                    
    ;display message about beginnig of work                
    display procStartStr
    
    ;open file and check if it opened correctly
    lea dx, cmdLine
    call fopen
    cmp ax, 1
    je endMain
    
    ;count non empty lines
    call countNonEmptyLines
    
    ;display amount of non empty lines
    displaySingle amountStr
    mov ax, nonEmptyLines
    call printDecNumber
    displaySingle endl
 
    ;close file
    ;end program
    call fclose
    display procFinStr
    
    jmp endMain
    
noData: 
    display noDataStr
       
endMain:
    display finStr 
    exit 
    
 
;                    PROCEDURES                     
 
;returns: operation code in ax (0 - ok, 1 - no args)
getComArgs proc 
    
    push ax
    push cx
    
    mov cx, 0
    
    ;gets length of cmd
    ;and checks if lenght is valid
	mov cl, es:[80h]	;80h - cmd length	
	mov cmdLen, cx
	cmp cx, 1
	jle endGCA 		           
    
    ;DF=0
	cld     
	
	mov di, 81h         ;81h - cmd itself
	mov al, ' '
	
	rep scasb   ;repeat send byte while not end
	dec di
	
	lea si, cmdLine
	
	;finding the end of cmd parametr line
skip:
    mov al,es:[di]
    cmp al, 0dh ;/r?
    je endSkip
    cmp al, 20h ;space?
    je endSkip 
    cmp al, 9h  ;tab?
    je endSkip
    mov ds:[si], al 
    inc di
    inc si
    jmp skip  
       	
endSkip:
    
    ;setting '$' at the end of cmd parametr line
    inc si
    mov ds:[si], word ptr '$'   
    
    ;end of procedure         
endGCA:
    pop cx
    pop ax    
    ret
endp getComArgs  


; requires: ds:dx - file name
; returns:  ax - operation code (0 - ok, 1 - error)
; fileDescr - file descr  
fopen proc

    push cx 

;   open file in read/write mode
    mov ah, 3dh
    mov al, 0h; read/write mode
    int 21h

;   if errors occured, handle them        
    jc errorHandling  
    
    mov fileDescr, ax ; copy desc
    jmp fopenOk

;   error handling   
errorHandling:

    cmp al, 02h
    jne pathNotFound
    display errMessage1
    jmp fopenError
    
pathNotFound:

    cmp al, 03h
    jne tooManyFilesOpened 
    display errMessage2
    jmp fopenError
    
tooManyFilesOpened:
                        
    cmp al, 04h
    jne accessDenied 
    display errMessage3
    jmp fopenError
    
accessDenied:

    cmp al, 05h
    jne wrongAccessMode 
    display errMessage4
    jmp fopenError
    
wrongAccessMode:

    cmp al, 0Ch
    jne fopenError
    display errMessage5
    jmp fopenError
     
fopenOk:
    mov ax, 0 
    jmp fopenEnd
fopenError: 
    mov ax, 1 
    
fopenEnd:
    pop cx    
    ret 
fopen endp



fclose proc
; requires: bx - file descr 
    mov bx, fileDescr
    mov ah, 3eh
    int 21h ;  
    
    jnc fcloseOk   
     
    mov ax, 1
    jmp fcloseFin 
    
fcloseOk :
    mov ax, 0 
fcloseFin:
    ret
fclose endp



countNonEmptyLines proc 
    pusha
    
    mov nonEmptyLines, 0
    mov chars, 0 
    mov addAmount, 0
    mov cf, 0 
    mov eof, 0
    mov bx, fileDescr
    
handling:
;   reads file
;   DX - result
;   CX - amount of read bytes 
;   AX - is opearion completed successfully
    call readFromFile ; fill buffer
    
    ;if we are at eof
    cmp ax, 1
    je finCounting
    cmp cx, bufSize   ; cx<bufSize -> not enough chars
    jb lastOperation  ; to fill the buf -> end of file

    call scanBuf      ; 

    jmp handling

lastOperation:
    mov eof, 1
    call scanBuf
    jmp finCounting
    
finCounting: 
    popa
    ret
countNonEmptyLines endp
                 
                 
;requires: bx - file descr
;returns:  ax - operation code (0 - ok, 1 - error)
;          cx - amount of read bytes
readFromFile proc  
;   BX = file handle.
;   CX = number of bytes to read.
;   DS:DX -> buffer for data.

    mov ax, 0
    mov ah, 3fh
    mov cx, 50 ; read 50 bytes if possible
    lea dx, buf 
    int 21h
    
    jc FRRerror 

;   saving the amount of read bytes    
    mov cx, ax
    mov ax, 0

    jmp FRRend

FRRerror:
    display errMessage7
    mov ax, 1

FRRend:
    ret
readFromFile endp


   
scanBuf proc
    lea si, buf
    push ax
    
scan:
    lodsb ;load byte
    
    ;comparing     
    cmp al, 13  ; \r?
    je setCF 

    cmp al, 10  ; \n?
    je handlEndl
    
    cmp al, 20  ; space?
    je nextB
    
    inc chars       
    jmp nextB

setCF:
    mov cf, 1
    jmp nextB

handlEndl:
;   if we hadn't \r we do nothing      
    cmp cf, 1
    jne nextB
;   else we look at our string
;   if we string were empty then we do nothing
;   else we count this string   
    mov cf, 0
    cmp chars, 0
    je nextB
    mov chars, 0
    inc nonEmptyLines
;   checking if we have overflowe
    jo handleOverflow
    jmp nextB 

handleOverflow:
    inc addAmount
    mov ax, addAmount
    call printDecNumber
    
    displaySingle addAmountStr
    
    dec nonEmptyLines 
    mov ax, nonEmptyLines
    call printDecNumber
    displaySingle endl    
    
    mov nonEmptyLines, 0
    inc nonEmptyLines
        
nextB:        
    loop scan
    
    cmp chars, 0
    je endSB
    cmp eof, 0
    je endSB
    mov chars, 0
    inc nonEmptyLines
    jo handleOverflow
     
endSB:
;   display sucMessage4 
    pop ax
    ret
scanBuf endp  
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
printDecNumber proc
;ax contains result
    pusha
    xor cx, cx
    mov bx, 10

;   transfer the number to stack       
loop1:        
    xor dx, dx
    div bx
    push dx 
    inc cx
    cmp ax, 0
    ja loop1
    
;   number to ascii and out    
loop2:
    pop dx
    add dx, 30h
    mov ah, 2
    int 21h
    loop loop2
    
    popa    
    ret    
printDecNumber endp 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
end main
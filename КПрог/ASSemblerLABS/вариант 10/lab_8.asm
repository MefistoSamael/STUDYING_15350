; multi-segment executable file template.
  
include 'emu8086.inc'
data segment 
    cmd_size db 0
    cmd_string db 130 dup('$')
    f_name1 db 20 dup('$')
    InputWordik db "Vvedite slovo",0Dh,0Ah,'$'
    str_end db  0Dh, 0Ah,0Dh, 0Ah,'$'
    error db "Error...$"
    error2 db "Error2...$"
    testik db 0Dh, 0Ah,"Tut...",0Dh, 0Ah,'$'
    buffer db ?,'$'
    wordik db 9,10 dup('$')
    fileWord db 11 dup('$')
    wordSize db 0
    answ db 0
    f_name db 'my_file.txt',0,'$'
    
ends

stack segment
   dw   128  dup(0)
ends

code segment
    
macro otl
    mov ah,9    
    lea dx,testik
    int 21h        
endm
    
macro clear_flags
    clc
    cld        
endm
;////////////////////////////////////////    
macro go_down
    mov ah,9    
    lea dx,str_end
    int 21h
endm
;////////////////////////////////////////

;////////////////////////////////////////    
macro readByte
    mov ah,3Fh
    int 21h
    jc ifError
endm
;////////////////////////////////////////

;////////////////////////////////////////
InputWord proc
    mov ah,9    
    lea dx,InputWordik
    int 21h        
    ;vvod stroki
    mov ah, 0ah             
    lea dx, wordik          
    int 21h
    go_down
    ret
InputWord endp
;////////////////////////////////////////

;////////////////////////////////////////
get_fileName proc   
  xor bx,bx 
  xor cx,cx
  mov cl,cmd_size 
  xor di,di
  xor ax,ax
  lea si,f_name1    
  loop00:  
    cmp cl,ch
    je tiz
    mov al,byte ptr cmd_string[di]  
    mov [bx+si],al
    inc bx
    inc di 
    inc ch 
    jmp loop00
 tiz:    
   dec bx
   mov [bx+si],0
   ret
get_fileName endp
;////////////////////////////////////////

;////////////////////////////////////////
proc readAndOut
    mov bx,ax
    mov ax,0
    mov cx,1
    mov si,0 ;index of wordik
    mov di,0 ;index of FileWord
    lea dx,buffer
    while1:
       readByte
       mov cx,ax
       jcxz endWhile1
       mov ah,0 
       mov al,buffer[0]
       cmp al,0Ah
       je incAnsw
       call if_litter
       jnc skip1
       push ax
       mov ah,buffer[0]
       mov FileWord[di],ah
       inc di
       mov ax,di
       cmp al,wordik[1]
       pop ax
       jna outlit
       call FindNewWord
       jmp while1 
    skip1:
       push ax 
       mov ax,di
       cmp al,wordik[1]
       pop ax
       jne skip2:
       mov ah,09h
       int 21h
       mov cx,1
       call CmpWord
       jmp while1
    incAnsw:
       inc answ[0]
    skip2:
       mov di,0
    outlit:
       mov ah,09h
       int 21h
       mov cx,1
       jmp while1
    endWhile1:
    inc answ[0]
    ret
readAndOut endp    
;////////////////////////////////////////

;///////////////////////////////////////
;nujno zanesti v AX byte, kotoriy proverim
;flag CF - resultat proverki
if_litter proc
    clear_flags
    cmp ax,36
    je ifError
    cmp ax,65
    jb clrC
    cmp ax,122
    ja clrC
    cmp ax,89
    jb iftrue
    cmp ax,97
    jb clrC
iftrue:
    stc
    jc endLitter
clrC:
    clc
endLitter:
    ret
if_litter endp
;////////////////////////////////////////

;////////////////////////////////////////
;0Dh ranshe!!!!!!!!
FindNewWord proc
    mov ah,09h
    int 21h
    while2:
        readbyte
        mov cx,ax
        jcxz endWhile1
        mov ah,09h
        int 21h
        mov ah,0 
        mov al,buffer[0]
        call if_litter
        jnc endWhile2
        mov cx,1
        jmp while2
    endWhile2:
    mov di,0 
    ret
FindNewWord endp
;////////////////////////////////////////

;////////////////////////////////////////
CmpWord proc
    push ax
    mov si,0
    while3:
        mov al,FileWord[si]
        cmp al,wordik[2+si]
        jne skip3
        inc si
        push ax
        mov ax,si
        cmp al,wordik[1]
        pop ax
        jne while3
  true:
     call FindEndStr
     dec answ[0]
  skip3:
    mov di,0
    pop ax 
    ret
CmpWord endp
;////////////////////////////////////////

;////////////////////////////////////////
FindEndStr proc
    while4:
        readbyte
        mov cx,ax
        jcxz skip5
        mov ah,09h
        int 21h
        mov cx,1
        cmp buffer[0],0Ah
        jne while4
    endWhile4:
    inc answ[0]
  skip5:     
    ret        
FindEndStr endp
;////////////////////////////////////////      
start:
; set segment registers:
    mov ax,@data
    mov es,ax 
    
    xor cx,cx
	mov cl,ds:[80h] 
	mov bl,cl	    
    cmp bl,0
    je ifError  
    dec cx
    mov si,82h
    mov di,offset cmd_string        
    rep movsb 
         
    mov ds,ax
     
        
    call get_fileName
    go_down
    
    mov ah,9    
    lea dx,f_name1
    int 21h
    
    go_down
    otl 
    
    mov ah,9    
    lea dx,InputWord
    int 21h        
    ;vvod stroki
    mov ah, 0ah             
    lea dx, wordik          
    int 21h
    go_down
    
    otl
    
    cmp wordik[1],0
    je ifError
    mov di,0
    while5:
        mov ah,0
        mov al,wordik[2+di]
        call if_litter
        jnc ifError
        inc di
        mov ax,di
        cmp al,wordik[1]
        jne while5
    endWhile5:
    
    mov dx,offset f_name1
    mov ah,3Dh
    mov al,00
    int 21h
    jc ifError
    call readAndOut
    go_down
    
    mov ah,0
    mov al,answ[0]
    call print_num_uns
    
close_file:
    mov ah,3Eh
    int 21h 
jmp stop    

ifEr2:
    mov ah,9    
   lea dx,error2
   int 21h     
   mov ah,3Eh
   int 21h
   jmp stop 
ifError:
   mov ah,9    
   lea dx,error
   int 21h     
   mov ah,3Eh
   int 21h 
   
stop:   
    mov ax, 4c00h ; exit to operating system.
    int 21h
    
    DEFINE_PRINT_NUM_UNS    
ends

end start ; set entry point and stop the assembler.

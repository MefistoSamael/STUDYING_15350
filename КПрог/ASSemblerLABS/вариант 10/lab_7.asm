; multi-segment executable file template.
include 'emu8086.inc'

data segment
    startProgr db "Vvedite razmer",0Dh,0Ah,'$'
    startMsg db "Vvedite chisla",0Dh,0Ah,'$'
    maxMsg db "Maximaknoe 4islo",0Dh,0Ah,'$'
    zn_in_dr db "Zna4enia v drobiah",0Dh,0Ah,'$'
    celoe db "Celoe  ",'$'
    zap db ",",'$'
    minusik db "-",'$'
    ost db "  ostatok  ",'$'
    error db "Plohoi vvod",0Dh,0Ah,'$'
    error2 db "Slishkom bolshoe dlia drobi",0Dh,0Ah,'$'
    str_end db  0Dh, 0Ah,'$'
    maxDigit dw ?
    Digits dw 32  dup (0)
    Answ dw 32  dup (0)
    sizeInput db ?
    maxSize db 4h
    maxN db 3h
    
ends

stack segment
    dw   128  dup(0)
ends

code segment
    
macro go_down
    mov ah,9    
    lea dx,str_end
    int 21h
endm

macro out_str msg
    mov ah,9    
    lea dx,msg
    int 21h
endm
    
macro clear_flags
    clc
    cld        
endm
    
macro abs
op:
    neg cx
    js op
endm


macro out_answ
    out_str celoe
    mov ax,Answ[di]
    call print_num
    out_str ost
    mov ax,Digits[di]
    call print_num
    go_down
endm        
;//////////////////////////////////////////////////
;//////////////////////////////////////////////////                                                  
input proc
    out_str startMsg
    clear_flags
    mov di,0
while1:
    go_down
    mov si,cx
    CALL scan_num
    mov Digits[di],cx
    ;abs
    cmp cx,maxDigit[0]
    jl contin
    mov maxDigit[0],cx
contin:
    mov cx,si
    inc di
    inc di
    loop while1
endDigit:
    ret
input endp
;/////////////////////////////////////////////////
    if_digit proc
    clear_flags
    mov dx,0
    mov al,maxSize[1]
    mov ah,0
    mov si,ax
    cmp maxSize[1],0
    je ifError
    cmp maxSize[2],45
    je minus
    cmp maxSize[1],1
    je third
    cmp maxSize[1],2
    je second
first:
    cmp maxSize[si-1],48
    jb ifError
    cmp maxSize[si-1],57
    ja ifError
    mov al,maxSize[si-1]
    sub al,48
    mov bl,100
    mul bl
    add dx,ax
    
second:
    cmp maxSize[si],48
    jb ifError
    cmp maxSize[si],57
    ja ifError
    mov al,maxSize[si]
    sub al,48
    mov bl,10
    mul bl
    add dx,ax
    
third:    
    cmp maxSize[si+1],48
    jb ifError
    cmp maxSize[si+1],57
    ja ifError
    mov al,maxSize[si+1]
    sub al,48
    add dx,ax
    jmp endif
minus:
    mov dx,1b
    cmp si,3
    je second
    jmp third 

endif:
    mov Digits[di],dx
    ret
    if_digit endp
;///////////////////////////////////////////////////
func proc
     mov cx,0
     mov di,0
    mov cl,sizeInput[0]
while2:
    mov ax,Digits[di]
    mov bx,maxDigit[0]
    clear_flags
    mov dx,0
    cmp ax,0
    jnl skip
    not dx
skip:
    idiv bx
    mov Digits[di],dx
    mov Answ[di],ax
    inc di
    inc di
    loop while2    
    ret
func endp
;/////////////////////////////////////////////////////
outResult proc
    mov cx,0
    mov di,0
    mov cl,sizeInput[0]
while3:
    out_answ

    inc di
    inc di
    loop while3    

    ret
outResult endp
;/////////////////////////////////////////////////////
drob proc
    mov cx,0
    mov di,0
    mov cl,sizeInput[0]
while4:
    mov si,3
    mov ax,maxDigit[0]
    cmp ax,0
    jl skip3
    mov ax,Digits[di]
    cmp ax,0
    jge skip3
    mov ax,Answ[di]
    cmp ax,0
    jl negat
    neg Digits[di]
    out_str minusik
    jmp skip3
negat:
    neg Digits[di]
skip3:
    mov ax,Answ[di]
    call print_num
    out_str zap
    while4_1:
        mov bx,10
        mov ax,Digits[di]
        cmp ax,0
        jb plust
        plust:
        cmp ax,3000
        jg ifError2
        jmp skip3_1
        minust:
        cmp ax,-3000
        jl ifError2
    skip3_1:
        mov dx,0
        cmp ax,0
        jnl skip4
        not dx
    skip4:
        imul bx
        mov bx,maxDigit[0]
        mov dx,0
        cmp ax,0
        jnl skip5
        not dx
    skip5:
        idiv bx
        call print_num
        mov Digits[di],dx
        dec si
        cmp si,0
        jne while4_1
    endWhile4_1:
    go_down    
    inc di
    inc di
    loop while4    

    ret
drob endp
;////////////////////////////////////////////////////
start:
; set segment registers:
    mov ax, data
    mov ds, ax
    mov es, ax
    
    mov dx,0
    mov maxDigit,dx
    
    out_str startProgr
    
    mov ah,0ah
    lea dx,maxN
    int 21h
    
    go_down

;//////proverka i perevod razmera////////
    mov ax,0
    mov bx,0
    mov ch,0
    mov cl,maxN[1]     ;razmer vvoda
    cmp cl,0
    je ifError
    cmp cl,1
    je oneDig
    cmp cl,2
    je twoDig
oneDig:
    mov bl,maxN[2]
    cmp bl,48
    jb ifError
    cmp bl,57
    ja ifError
    sub bl,48
    mov di,bx
    jmp endCheck
    
twoDig:
    mov bl,maxN[2]
    cmp bl,48
    jb ifError
    cmp bl,51
    ja ifError
    sub bl,48
    mov al,bl
    mov bl,10
    mul bl
    mov di,ax
    cmp maxN[2],51
    je first3
    ;vtoryu cifru smotrim
    mov bl,maxN[3]
    cmp bl,48
    jb ifError
    cmp bl,57
    ja ifError
    sub bl,48
    add di,bx
    jmp endCheck
    
first3:
    mov bl,maxN[3]
    cmp bl,0
    jne ifError
    
endCheck:
    cmp di,0
    je ifError
    ;v cx lejit kol-vo vvodov v array
    mov cx,di
    mov sizeInput[0],cl
    call input
    
    ;CALL scan_num
    go_down 
    out_str maxMsg
    go_down
    mov ax,maxDigit[0]
    CALL print_num
    go_down
    mov ax,maxDigit[0]
    cmp ax,0
    je ifError
    
    call func
    
    call outResult
    go_down
    go_down
    out_str zn_in_dr
    call drob
    
    jmp stop            
    
    
ifError:
   go_down
   mov ah,9    
   lea dx,error
   int 21h     
   jmp stop 
    
ifError2:
   go_down
   mov ah,9    
   lea dx,error2
   int 21h     
   jmp stop 

stop:

    mov ax, 4c00h ; exit to operating system.
    int 21h    
ends

DEFINE_SCAN_NUM
DEFINE_PRINT_NUM
DEFINE_PRINT_NUM_UNS

end start ; set entry point and stop the assembler.

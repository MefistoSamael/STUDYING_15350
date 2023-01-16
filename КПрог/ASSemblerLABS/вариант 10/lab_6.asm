; multi-segment executable file template.

data segment
   string  db  200, 202  dup ('$')
   wordik  db  200, 202  dup ('$')
   str_end db  0Dh, 0Ah,0Dh, 0Ah,'$'
   InputStr db "Vvedite stroku",0Dh,0Ah,'$'
   InputWord db "Vvedite slovo",0Dh,0Ah,'$'
   error db "Plohoi vvod",0Dh,0Ah,'$'
ends

stack segment

ends

code segment
macro clear_flags
    clc
    cld        
endm

macro go_down
    mov ah,9    
    lea dx,str_end
    int 21h
endm

;//////////////////////////////////////////
macro input
    mov ah,9    
    lea dx,InputStr
    int 21h        
    ;vvod stroki
    mov ah, 0ah             
    lea dx, string          
    int 21h
    go_down
    mov ah,9    
    lea dx,InputWord
    int 21h        
    ;vvod stroki
    mov ah, 0ah             
    lea dx, wordik          
    int 21h
    go_down    
endm
;//////////////////////////////////////////
;//////////////////////////////////////////

;/////////////////////////////////////////
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
;//////////////////////////////////////////
;//////////////////////////////////////////
start:
; set segment registers:
    mov ax, data
    mov ds, ax
    mov es, ax
    mov ax, stack
    mov ss, ax
    mov cx,0 
    jmp fin
    input
    mov al,wordik[1]
    mov ax, 202
    push ax
    
    mov ax,0
    
    mov cx,0
    
    cmp cl, string[1]
    je ifError
    mov si,cx
    mov al,string[2+si]
    call if_litter
    jc pushIndexStart
while1:
    ;inc cx
    cmp cl, string[1]
    je endwhile1
    ;dec cx
    ;if(!macros(sent[i-1]) && macros(sent[i])) 
firstCheck:
    dec cx
    mov si, cx
    inc cx
    mov al,string[2+si]
    call if_litter
    jc secondCheck
    mov si, cx
    mov al,string[2+si]
    call if_litter
    jc pushIndexStart
secondCheck:
    mov si,cx
    mov al,string[2+si]
    call if_litter
    jc plus1
    dec cx
    mov si, cx
    inc cx
    mov al,string[2+si]
    call if_litter
    jc pushIndexEnd
    jmp plus1
pushIndexStart:
    push cx
    jmp plus1
pushIndexEnd:
    dec cx
    push cx
    inc cx
plus1:
    inc cx
    jmp while1
endwhile1:
    dec cx    
    mov si,cx
    mov al,string[2+si]
    call if_litter
    jnc skip1
    ;dec cx
    push cx
    inc cx
skip1:
    
    mov ax,0 ;false
while2:
    pop cx
    mov ch,0
    cmp cx,202
    je endwhile2
    ;mov ax,wordik[1]
    ;pop cx
    mov si,cx
    mov di,0
    mov cl,wordik[1]
    add di,cx
    add di,1
    mov ax,1
    mov bx,sp
    mov cx,si
    sub cx,ss:[bx]
    inc cx
    ;esli slova raznoi dlinni
    cmp cl,wordik[1]
    jne fail
    mov cx,si
    while2_1:
        cmp cx,ss:[bx]
        jb endWhile2_1
        ;mov al, wordik[di]
        mov dl,string[2+si]
        cmp dl,wordik[di]
        mov cx,si
        je plus2
        mov ax,0
        pop cx
        jmp while2
    plus2:
        dec cx
        mov si,cx
        dec di
        jmp while2_1
    endWhile2_1:
        cmp ax,1
        je endwhile2
    pop cx
    cmp cx,202
    je endwhile2
    jmp while2
fail:
    mov ax,0
    jmp while2
endwhile2:    
    
    cmp ax,0
    je ifError
    
    pop cx
    mov ax,0
    mov dx,0
    pop si
    cmp si,202
    je ifError
    
    ;pop si
    inc si
    pop bx
    
while3:
    cmp bl,string[1]
    je endWhile3
    mov ah,string[2+bx]
    mov al,string[2+si]
    mov string[2+si],ah
    mov string[2+bx],al
    
    inc si
    inc bx
    jmp while3
endWhile3:
    mov ah,string[2+bx]
    mov al,string[2+si]
    mov string[2+si],ah
    mov string[2+bx],al
    
    lea dx, string[2]
    mov ah, 9h
    int 21h
fin:

    mov ax,-1
    inc ax
    dec ax
    dec ax    
jmp stop    
    
ifError:
   mov ah,9    
   lea dx,error
   int 21h     
   jmp stop 
    
 

stop:

    mov ax, 4c00h ; exit to operating system.
    int 21h
        
ends

end start ; set entry point and stop the assembler.

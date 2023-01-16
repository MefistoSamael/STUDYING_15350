
.model small                             

.code                                   
   
swap macro x1,x2
   
    mov ah,x1
    mov al,x2
    
    mov x1,al
    mov x2,ah 
    
endm
    
    mov ax,@data                     
    mov ds,ax 
    
    mov ah,9    
    lea dx,esys
    int 21h
    
    mov ah, 0ah 
    lea dx, ent_max          ; vvod stroki 
    int 21h  
    
    call endl   
     
    cmp ent_len,0
    je Error 
     
    cmp ent_len,2
    je s1 
     
    cmp ent_str[0],'2'
    jl  Error
    
    cmp ent_str[0],'9'
    jg  Error
    
    mov ah,0
    mov al,ent_str[0]
    sub ax,48
    
    mov sys_ent,ax
     
    jmp s2
s1: 
    cmp ent_str[0],'1'
    jne  Error 
     
    cmp ent_str[1],'6'
    jg  Error
    
    cmp ent_str[1],'0'
    jl  Error  
     
    mov ah,0
    mov al,ent_str[1]
    sub ax,38
    
    mov sys_ent,ax  
     
s2:     
    mov ah,9    
    lea dx,osys
    int 21h
         
    mov ah, 0ah 
    lea dx, out_max          ; vvod stroki 
    int 21h  
  
    
    call endl
   
    cmp out_len,0
    je Error 
     
    cmp out_len,2
    je c1 
     
    cmp out_str[0],'2'
    jl  Error
    
    cmp out_str[0],'9'
    jg  Error
    
    mov ah,0
    mov al,out_str[0]
    sub ax,48
    
    mov sys_out,ax
     
    jmp c2
c1: 
    cmp out_str[0],'1'
    jne  Error 
     
    cmp out_str[1],'6'
    jg  Error
    
    cmp out_str[1],'0'
    jl  Error  
     
    mov ah,0
    mov al,out_str[1]
    sub ax,38
    
    mov sys_out,ax       
c2:     
    mov ah,9
    lea dx,inp
    int 21h
    
    mov ah, 0ah             
    lea dx, str_max          ; vvod stroki 
    int 21h   
  
    cmp str_len,0
    je Error
  
  
    mov dl,8
    sub dl,str_len
    
    cmp dl,0
    je skp
    
k:    
    call mov_r
    dec dl
    cmp dl,0
    jne k 
    
skp: 



         
    mov si,0        
l0:      
    call chek_char
    
    inc si
    cmp si,8
    jl l0 
                  
    mov i,8        
            
L1:
    
    mov bx,i  
    
    call put
    
    mov tmp1,ax
       
    mov cx,i 
    mov j,cx 

    cmp j,8
    je skip0
    
L2:  
    
    cmp j,4
    jle skip1
    
    mov ax,tmp1   
    mul sys_ent
    mov tmp1,ax 
    
    ;push j
     
    jmp skip2 
skip1:
    
    mov ax,tmp2
    mul sys_ent
    mov tmp2,ax 
       
    ;push j

    
skip2:
       
    inc j   
       
    cmp j,7
    jle L2

skip0:            
    
    mov ax,tmp1
    mul tmp2
    
    push dx
    push ax
      
    mov tmp2,1
            
    dec i      
    cmp i,0
    jg L1        
            
    mov cx,7
L3:
    
    call sum
    
    loop L3 

    
    cmp sys_ent,16
    jne skip3
    
    mov bx,1
    call put
    shl al,4 
     
    mov ch,al 
    
    pop bx
    pop ax
    
    add ah,ch
    
    push ax
    push bx
  
skip3:    
                        
    pop cx  ;mlad         b
    pop ax  ;starsh       c
    
    
    mov dx,0 

    mov si,0
    
L4:
    call looop
        
    cmp ax,0        
    jne L4
         
    cmp cx,0       
    jne L4
  
  
    mov bx,0
    
    mov dx,si  
    mov cx,si
    shr dl,1 
    dec si

L6:    
    swap new_num[bx],new_num[si]  
 
    inc bx 
    dec si
    
    cmp bx,dx
    jl L6
    
    mov si,cx
               
    mov new_num[si],0Ah 
    mov new_num[si+1],0Dh
    mov new_num[si+2],'$'
    
    mov ah,9    
    lea dx,str_ent
    int 21h
    
    mov ah,9    
    lea dx,ans
    int 21h 
    
    mov ah,9    
    lea dx,new_num
    int 21h  
                           
    mov ax,4C00h                       
    int 21h

Error:
    
    call endl  
    
    mov ah,9    
    lea dx,eror
    int 21h 
    
    mov ax,4C00h                       
    int 21h 





  
mov_r proc
    
    mov cx,7
l9:  
    mov si,cx
    
    swap string[si],string[si-1]
    
    loop l9
    
    mov string[0],'0'
    
    ret
    
mov_r endp    
  
  
endl proc
    
    mov ah,9    
    lea dx,str_ent
    int 21h
    
    ret   
    
endl endp      
  
  
                                 
looop proc
             
    div sys_out  
    
    mov bx,ax
    mov ax,cx
    
    div sys_out
    
    call set
     
    mov new_num[si],dl 
    inc si
    
    mov cx,ax
    mov ax,bx
    mov dx,0
        
    ret      
          
looop endp    

chek_char proc 
    
    cmp sys_ent,9
    jge s4
    
    mov dl,ent_str[0]
    
    cmp string[si],dl
    jge Error
    
    cmp string[si],'0'
    jl Error

s4:
    cmp string[si],'9'
    jge s5
    
    cmp string[si],'0'
    jl Error
    
    ret     
s5:
    mov dx,sys_ent
    add dx,'A'
    sub dx,10
    
    cmp string[si],dl
    jge Error
    
    cmp string[si],'A'
    jl Error 
      
    ret
    
chek_char endp 

set proc
   
    cmp dl,9
    jg letr
    
    add dl,48
    
    ret
letr:  
    add dl,55
        
    ret 
    
set endp    


sum proc 

    pop ax
    mov tmp1,ax
    
    pop ax    
    pop bx
    pop tmp2
    pop dx
    
    add ax,tmp2
    adc bx,dx
    
    push bx
    push ax    
    
    push tmp1
       
    ret

sum endp


put proc
    
    mov ah,0
    mov al,string[bx-1]
       
    cmp al,'9'
    jg leter
    
    sub ax,48
    
    ret
leter:
    
    cmp al,'F'
    jg mini_leter    
    sub ax,55 
    
    ret 
mini_leter: 

    sub ax,87 
    
    ret

     
put endp

    .stack 100h
    
    .data   
ent_max db  3
ent_len db  0
ent_str db  16 dup ('-')    
se_ent  db  '$' 

out_max db  3
out_len db  0
out_str db  16 dup ('-')    
so_ent  db  '$'
                                
str_max db  9                                          ; makcimalnaia dlina stroki
str_len db  0                                          ; dlina posle vvoda
string  db  9  dup ('0')                               ; bufer
s_ent   db  "00000000",0Dh,0Ah,'$'                 ; /n+/0  
buf     db  4  dup (0) 
number  db  8  dup (0)

str_ent db  0Dh, 0Ah,0Dh,0Ah,'$'
 
esys    db  "Entering system: $" 
osys    db  "Outpuing system: $" 
eror    db  "Input error$"
inp     db  "Input: $"
ans     db  "Answer: $"
 
sys_ent dw  16 
sys_out dw  10
i       dw  0 
j       dw  0
tmp1    dw  1                            
tmp2    dw  1
new_num db  32 dup ('0')
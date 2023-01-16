
    .model tiny                                                          
    org 100h                              
  
start:                                   
      
    mov ah,9    
    lea dx,stard
    int 21h  
                                               
    mov ah, 0ah             
    lea dx, str_max          ; vvod stroki 
    int 21h        
    
    lea dx,string                                
    push dx 
    
    mov ah,0
    mov al,str_len                  
    
    add dx,ax                       ; v stek tolkaem nachalo i konec
    push dx
    
    sub dx,ax
    
                   
    call Qsort                      ; zapuskaem rekursiu
       
    mov ah,9     
       
    lea dx,str_ent                                                                                                                      
    int 21h 
    lea dx,ent 
    int 21h 
    lea dx,string 
    int 21h                         ; vivodim otsortirovannuu stroku  
                             
    mov ax,4C00h                        
    int 21h                         ; zakanchivaem programmu
         
Qsort     PROC
   
   pop ax                           ; zabiraem adres next proceduri 
    
   pop bx                           ; beriom nachalo i konec
   pop si
         
   push ax                          ; vozvraschaem adres proceduri
         
   mov di,si                        ; scherchiki, kotorie idut k seredine
   mov bp,bx                        ; di sleva, bp sprava
   
   call Pivot                       ; v cx kladiom znachenie iz seredini
            
         
                      
   cmp di,bp                         
   jg skip1      
                                    ; poka di menshe bp, idet cikl
loop1:   
   
   cmp cl,[di]                      
   jle skip2
   

loop2:                              ;nahodim sleva znachenie, bolshee chem srednee
    
   inc di
   
   cmp cl,[di]
   jg loop2
   
   
skip2:  

   cmp cl,[bp]
   jge skip3 
                                    ; nahodim sprava znachenie, menshee chem srednee
loop3:

   dec bp
   
   cmp cl,[bp]
   jl loop3 
                
skip3:   
    
    
    cmp di,bp                       ; esli s indexami vse ok, swapaem
    jg skip4
         
    mov ah,[di]
    mov al,[bp]
    
    mov [di],al
    mov [bp],ah     

     
    inc di
    dec bp 
     
skip4:
      
    cmp di,bp                       ; esli di i bp esche ne soshlis, prodoljaem swapat dalshe
    jle loop1  
        
skip1:      

    push bx                         ; kogda swapnuli vse, chto mogli, zapuskaem rekursiu
    push di                         ; dlia kajdoi polovini,esli nado 
    
    cmp si,bp
    jge skip6
      
    push si
    push bp
    
    call Qsort  
    
skip6:
        
    pop di
    pop bx
    
    cmp di,bx
    jge skip7
    
    push di
    push bx
    
    call Qsort
    
skip7:    
                
    RET 
         
Qsort     ENDP  
      
      
      
Pivot proc                      ; nahojdenie seredini podmassiva
    
    mov ax,si                   ; polovina nachala
    shr ax,1
      
    mov cx,ax
    
    mov ax,bx                   ; polovina konca
    shr ax,1
    
    add cx,ax                   ; seredina
    
    push bx
    mov bx,cx
    
    mov cl,[bx]                 ; v cx zanosim znachenie iz seredini
    
    pop bx
    
    ret
              
Pivot endp

 
str_max db  201                                          ; makcimalnaia dlina stroki
str_len db  0                                            ; dlina posle vvoda
string  db  201  dup (' ')                               ; bufer
str_ent db  0Dh, 0Ah,0Dh, 0Ah,'$'                        ; /n+/0   
stard   db  "Enter the string: ",0Dh, 0Ah,'$'
ent     db  "Answer: ",0Dh, 0Ah,'$'
                                         
end start  
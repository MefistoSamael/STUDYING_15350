
.model small                            ; zadaiom model. small = segment code + segment data   

.code                                   ; nachalo segmenta code                                 
    
    mov ax,@data                         
    mov ds,ax  
    
    lea dx,f_name ;                     ; adres ASCIZ-stroki with file name  
    
    mov ah,3Dh                          ; otkrit suschestvuuschiõ file
    mov al,00h                          ; tolko dla chtenia
    int 21h                                                           
    
    jc exit                             ; esli oshibka - vihod
    mov bx,ax                           ; bx - file identifikator 
    mov di,01                           ; di - STDOUT identifikator
                                        ; chtenie of data, zapis v STDOUT
read_data:
       
    mov cx,1                            ; razmer bloka dla chtenia
    lea dx,buffer                       ; bufer
    mov ah,3Fh
    int 21h    
    
    jc close_file                       ; esli oshibka - zakrit file
       
    mov si,dx
    cmp [si],0Dh                        ; sravnenie s 0Dh - perenosom karetki
    jne skip
    
    inc counter
skip:    
                     
    mov cx,ax                           ; cx - count of prochitanih byte
    
    jcxz close_file                     ; esli cx = 0 => zakrit file

    jmp short read_data                 ; read next symbol
    
close_file:

    mov ah,3Eh                          ; zakritie of file
    int 21h  
                     
    mov ah,9  
                                        
    lea dx,ans                           
    int 21h 
    
    lea dx,counter                      ; vivod of string count     
    int 21h   

exit:
    
    mov ax,4C00h                        ; konec progi
    int 21h                             
    
    .data                              
f_name  db 'data.txt',0                 
counter db 49,'$' 
buffer  db 0 
ans     db "Count of strings in file: $"   
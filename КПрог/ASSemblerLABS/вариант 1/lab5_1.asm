
    .model tiny                            ; tiny = vse v odnom segmente. small = segment code + segment data                                 ; 
    org 100h                               ; COM rezerviruet pervie 100 byte pod chto-to vajnoe 
  
start:                                     ; nachalo programmi
                                                                                
    mov Ah,9                               ; kod kommandi dla vivoda stroki iz Dx                    
    lea Dx,message                         ; zanosim v Dx massiv.lea = mov + offset,that beriot adres peremennoi       
    mov dx,offset message
    int 21h                                
    ret                                    ; vihod
message db "Hello World!",0Dh,0Ah,'$'      ; 0Dh karetku v nachalo
                                           ; 0Ah karetku vniz  
end start                                  ; db rezerviruet 1 byte      
                                           ; $ = (C++)/0, stroka zapisivaetsa kak massiv bytov   
                                           ; end eto konec programmi
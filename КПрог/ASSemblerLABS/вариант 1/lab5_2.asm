
.model small                            ; zadaiom model. small = segment code + segment data   

.code                                   ; nachalo segmenta code 

start:                                  ; nachalo programmi

    mov ax,@data                        ; 
    mov ds,ax                           ; zanosim v SEGMENTNô register ds adres SEGMENTA   
    
    lea dx,message               ;
    mov ah,9                            ;
    int 21h                             ; vizov vivoda na ekran stroki message 
    
    mov ax,4C00h                        ;
    int 21h                             ; 4C0H znachit vihod v operacionku
    
    .data                               ; segment data
message db "Hello World!",0Dh,0Ah,'$'   ; sozdaiom massiv 

end start                               ; konec programmi

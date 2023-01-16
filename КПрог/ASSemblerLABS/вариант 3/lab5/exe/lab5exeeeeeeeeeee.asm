data segment
msg db "first string",0Dh,0Ah, "second string",0Dh,0Ah,"third string", "$"    
ends

stack segment
    
ends

code segment
start:
 MOV bx, @data
 MOV ds, bx  
 MOV ah, 9h
 lea dx, msg  
 int 21h
 ret

ends

end start 

.model tiny
.code

org  0100h
MOV ah, 9h
lea dx, msg  
int 21h
ret

msg db "first string",0Dh,0Ah, "second string",0Dh,0Ah,"third string", "$"







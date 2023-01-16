 org $8000
 
; 1)

 ldaa #%00010001  
 staa $10

 ldaa #%01100000   
 oraa $10

 staa $10 
 
; 2)

 ldaa #%11010001
 staa $10
 
 ldaa #%10011111
 anda $10
 adda #%01100000

 staa $10 

; 3)

 ldaa #%11010001
 staa $10

 ldaa $10
 oraa #%10011111
 coma
 adda $10

 staa $10
 


		org $8000
;position-independent implementation

 ldaa #$AA
 ldab #$55
 psha
 pshb
 pula
 pulb
 xgdx


;staa $8200
;tba
;ldab $8200
;xgdx
 
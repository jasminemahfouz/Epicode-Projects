var User3 = /** @class */ (function () {
    function User3(nome, cognome) {
        this.nome = nome;
        this.cognome = cognome;
        this.credito = 0; 
        this.numeroChiamate = 0; 
    }
    User3.prototype.ricarica = function (ammontare) {
        this.credito += ammontare;
    };
    User3.prototype.chiamata = function (minuti) {
        var costoChiamata = minuti * 0.20;
        if (this.credito >= costoChiamata) {
            this.credito -= costoChiamata;
            this.numeroChiamate += minuti;
        }
        else {
            console.log("Credito insufficiente per effettuare la chiamata.");
        }
    };
    User3.prototype.chiama = function () {
        return this.credito;
    };
    User3.prototype.getNumeroChiamate = function () {
        return this.numeroChiamate;
    };
    User3.prototype.azzeraChiamate = function () {
        this.numeroChiamate = 0;
    };
    return User3;
}());
var utente = new User3("Carlo", "Mahfouz");
utente.ricarica(30); 
utente.chiamata(5);
utente.chiamata(10); 
console.log("Credito residuo:", utente.chiama());
console.log("Numero di chiamate:", utente.getNumeroChiamate());
utente.azzeraChiamate();
console.log("Numero di chiamate dopo azzeramento:", utente.getNumeroChiamate()); 

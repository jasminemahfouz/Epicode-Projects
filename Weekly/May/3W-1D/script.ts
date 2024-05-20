interface phone {
    credito: number;
    numeroChiamate: number;
  }
  
  class User3 implements phone {
    nome: string;
    cognome: string;
    credito: number;
    numeroChiamate: number;
  
    constructor(nome: string, cognome: string) {
      this.nome = nome;
      this.cognome = cognome;
      this.credito = 0;
      this.numeroChiamate = 0; 
    }
  
    ricarica(ammontare: number) {
      this.credito += ammontare;
    }
  
    chiamata(minuti: number) {
      const costoChiamata = minuti * 0.20; // Costo fisso di 20 cent/minuto
      if (this.credito >= costoChiamata) {
        this.credito -= costoChiamata;
        this.numeroChiamate += minuti;
      } else {
        console.log("Credito insufficiente per effettuare la chiamata.");
      }
    }
  
    chiama(): number {
      return this.credito;
    }
  
    getNumeroChiamate(): number {
      return this.numeroChiamate;
    }
  
    azzeraChiamate() {
      this.numeroChiamate = 0;
    }
  }
  
  const utente1 = new User3("Carlo", "Mahfouz");
  utente1.ricarica(30);
  utente1.chiamata(5);
  utente1.chiamata(10);
  console.log("Credito residuo:", utente1.chiama());
  console.log("Numero di chiamate:", utente1.getNumeroChiamate());
  utente1.azzeraChiamate();
  console.log("Numero di chiamate dopo azzeramento:", utente1.getNumeroChiamate());
  

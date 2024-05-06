class User {
    constructor(firstName, lastName, age, location) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.location = location;
    }

    confrontaEta(altroUtente) {
        if (this.age > altroUtente.age) {
            return `${this.firstName} è più vecchio di ${altroUtente.firstName}`;
        } else if (this.age < altroUtente.age) {
            return `${this.firstName} è più giovane di ${altroUtente.firstName}`;
        } else {
            return `${this.firstName} ha la stessa età di ${altroUtente.firstName}`;
        }
    }
}

// Creazione di due oggetti utente
let utente1 = new User("Yasmin", "Mahfouz", 39, "Napoli");
let utente2 = new User("Giulia", "Vecchi", 24, "Roma");

console.log(utente1.confrontaEta(utente2));

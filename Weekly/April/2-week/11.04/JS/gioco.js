
    // generare il tabellone
    function generaTabellone() {
      const tabellone = document.getElementById("tabellone");
      const tbody = tabellone.getElementsByTagName("tbody")[0];
      let numero = 1;
      for (let i = 0; i < 10; i++) {
        const row = document.createElement("tr");
        for (let j = 0; j < 8; j++) {
          const cell = document.createElement("td");
          cell.textContent = numero;
          cell.dataset.numero = numero;
          row.appendChild(cell);
          numero++;
        }
        tbody.appendChild(row);
      }
    }

    // estrarre un numero random e evidenziare la cella corrispondente
    function estraiNumero() {
      const numeroEstratto = Math.floor(Math.random() * 76) + 1;
      const cellaEstratta = document.querySelector(`#tabellone td[data-numero="${numeroEstratto}"]`);
      if (cellaEstratta) {
        cellaEstratta.classList.add("highlight");
      }
    }

    // generare e aggiungere una tabella separata
    function aggiungiTabellaSeparata() {
      const tabelloneSeparato = document.createElement("table");
      const tbody = document.createElement("tbody");
      tabelloneSeparato.appendChild(tbody);

      for (let i = 0; i < 6; i++) {
        const row = document.createElement("tr");
        for (let j = 0; j < 4; j++) {
          const cell = document.createElement("td");
          const numeroCasuale = Math.floor(Math.random() * 76) + 1;
          cell.textContent = numeroCasuale;
          row.appendChild(cell);
        }
        tbody.appendChild(row);
      }

      document.body.appendChild(tabelloneSeparato);
    }

    // reset
    function resetGame() {
      const celleEvidenziate = document.querySelectorAll('.highlight');
      celleEvidenziate.forEach(cella => {
        cella.classList.remove('highlight');
      });

      const tabelleSeparate = document.querySelectorAll('table:not(#tabellone)');
      tabelleSeparate.forEach(tabella => {
        tabella.remove();
      });
    }

    //generazione del tabellone principale quando la pagina è completamente caricata
    document.addEventListener("DOMContentLoaded", function() {
      generaTabellone();
    });

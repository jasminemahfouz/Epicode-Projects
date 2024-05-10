
const generateVynilCards = function (vinylArray) {
  const row = document.getElementById('events-row')
  vinylArray.forEach((vinyl) => {
    const newCol = document.createElement('div')
    newCol.classList.add('col')
    newCol.innerHTML = `
    <a href="details.html?eventId=${vinyl._id}" class="text-decoration-none text-dark">
      <div class="card h-100 d-flex flex-column">
        <img src="${vinyl.imageUrl}" class="card-img-top" alt="">
        <div class="card-body d-flex flex-column justify-content-around">
          <h5 class="card-title">${vinyl.name}</h5>
          <p class="card-text">${vinyl.description}</p>
          <p class="card-text">${vinyl.brand}</p>
          <div class="d-flex justify-content-between">
            <button class="btn btn-dark">${vinyl.price}€</button>
            <a href="backoffice.html?eventId=${vinyl._id}" class="btn btn-secondary" id="btn-info">Modifica</a>
          </div>
        </div>
      </div></a>
      `
    row.appendChild(newCol)
  })
}

const getEvents = function () {
  fetch('https://striveschool-api.herokuapp.com/api/product/',{
    headers: {
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2NjNkZDM0ZDgxODQ0MjAwMTUzNzU4N2UiLCJpYXQiOjE3MTUzMjc4MjIsImV4cCI6MTcxNjUzNzQyMn0.8DHR7-7DiVqm8THFiyI0vKTYXfLCwJfGYNIOljQC4Go',
    }
  })
    .then((response) => {
      if (response.ok) {
        console.log(response)
        return response.json()
      } else {
        throw new Error('Errore nella risposta del server')
      }
    })
    .then((array) => {
      console.log('ARRAY!', array)
      generateVynilCards(array)
    })
    .catch((err) => {
      console.log('ERRORE!', err)
    })
}

getEvents()

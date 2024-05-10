const addressBarContent = new URLSearchParams(location.search)
console.log(addressBarContent)
const eventId = addressBarContent.get('eventId')

const getEventData = function () {
  fetch(`https://striveschool-api.herokuapp.com/api/product/${eventId}`,{
    headers: {
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2NjNkZDM0ZDgxODQ0MjAwMTUzNzU4N2UiLCJpYXQiOjE3MTUzMjc4MjIsImV4cCI6MTcxNjUzNzQyMn0.8DHR7-7DiVqm8THFiyI0vKTYXfLCwJfGYNIOljQC4Go',
    }
  })
    
    .then((response) => {
      if (response.ok) {
        return response.json()
      } else {
        throw new Error("Errore nel recupero dei dettagli dell'evento")
      }
    })
    .then((event) => {
      console.log('DETTAGLI RECUPERATI', event)
      
      document.getElementById('imageUrl').innerText = event.imageUrl
      document.getElementById('name').innerText = event.name
      document.getElementById('brand').innerText = event.brand
      document.getElementById('description').innerText = event.description
      document.getElementById('price').innerText = event.price + '€'
    })
    .catch((err) => {
      console.log('ERRORE', err)
    })
}

getEventData()
const deleteEvent = function () {

  fetch(`https://striveschool-api.herokuapp.com/api/product/${eventId}`,{
  method: 'DELETE',
  headers: {
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2NjNkZDM0ZDgxODQ0MjAwMTUzNzU4N2UiLCJpYXQiOjE3MTUzMjc4MjIsImV4cCI6MTcxNjUzNzQyMn0.8DHR7-7DiVqm8THFiyI0vKTYXfLCwJfGYNIOljQC4Go',
    }
    
  })
    .then((response) => {
      if (response.ok) {
        alert('RISORSA ELIMINATA')
        location.assign('/exam/index.html')
      } else {
        alert('ERRORE - RISORSA NON ELIMINATA')
      }
    })
    .catch((err) => {
      console.log('ERR', err)
    })
}

const editButton = document.getElementById('btn-modify')
editButton.addEventListener('click', function () {
  location.assign(`/exam/backoffice.html?eventId=${eventId}`)
})
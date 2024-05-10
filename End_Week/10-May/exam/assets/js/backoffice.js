class Vinyl {
    constructor(_imageUrl, _name, _brand , _description,  _price) {
      this.imageUrl = _imageUrl
      this.name = _name
      this.brand = _brand
      this.description = _description
      this.price = _price
    }
  }
  const addressBarContent = new URLSearchParams(location.search) 
  const eventId = addressBarContent.get('eventId')
  console.log('EVENTID?', eventId)

  let eventToModify
  
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

        document.getElementById('imageUrl').value = event.imageUrl
        document.getElementById('name').value = event.name
        document.getElementById('brand').value = event.brand
        document.getElementById('description').value = event.description
        document.getElementById('price').value = event.price

        eventToModify = event
      })
      .catch((err) => {
        console.log('ERRORE', err)
      })
  }
  
  if (eventId) {
    getEventData()
    document.getElementsByClassName('btn-primary')[0].innerText = 'MODIFICA!'
  }
  

  const submitEvent = function (e) {
    e.preventDefault()
    const imageInput = document.getElementById('imageUrl') 
    const nameInput = document.getElementById('name')
    const brandInput = document.getElementById('brand') 
    const descriptionInput = document.getElementById('description') 
    const priceInput = document.getElementById('price') 
                                
  
    const vinylFromForm = new Vinyl(
      imageInput.value,
      nameInput.value,
      brandInput.value,
      descriptionInput.value,
      priceInput.value,
    )
  
    console.log('Vinili da inviare ad API', vinylFromForm)
  
    let URL = 'https://striveschool-api.herokuapp.com/api/product/'
    let methodToUse = 'POST'
  
    if (eventId) {
      URL = `https://striveschool-api.herokuapp.com/api/product/${eventToModify._id}`;
      methodToUse = 'PUT';
      Authorization = 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2NjNkZDM0ZDgxODQ0MjAwMTUzNzU4N2UiLCJpYXQiOjE3MTUzMjc4MjIsImV4cCI6MTcxNjUzNzQyMn0.8DHR7-7DiVqm8THFiyI0vKTYXfLCwJfGYNIOljQC4Go';

    }
  
    fetch(URL, {
      method: methodToUse,
      body: JSON.stringify(vinylFromForm),
      headers: {
        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2NjNkZDM0ZDgxODQ0MjAwMTUzNzU4N2UiLCJpYXQiOjE3MTUzMjc4MjIsImV4cCI6MTcxNjUzNzQyMn0.8DHR7-7DiVqm8THFiyI0vKTYXfLCwJfGYNIOljQC4Go',
        'Content-type': 'application/json',
      },
    })
      .then((response) => {
        if (response.ok) {
          alert(`Vinile ${eventId ? 'modificato' : 'creato'}!`)
          location.assign('/exam/index.html')
        } else {
          throw new Error('Errore nel salvataggio della risorsa')
        }
      })
      .catch((err) => {
        console.log('ERRORE', err)
        alert(err)
      })
  }

  const resetForm = function (event) {
    event.preventDefault();
    const isConfirmed = confirm("Sei sicuro di voler resettare il modulo?");
    if (isConfirmed) {
      document.getElementById('event-form').reset();
      document.getElementById('imageUrl').value = '';
      document.getElementById('name').value = '';
      document.getElementById('brand').value = '';
      document.getElementById('description').value = '';
      document.getElementById('price').value = '';
    }
  }
  
  document.getElementById('reset-button').addEventListener('click', resetForm);
  
  

document.getElementById('event-form').addEventListener('submit', submitEvent)
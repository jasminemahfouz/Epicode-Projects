document.getElementById("loadImages").addEventListener("click", function() {
    loadImages("cats");
  });

  document.getElementById("loadSecondaryImages").addEventListener("click", function() {
    loadImages("cars");
  });

  function loadImages(query) {
    const apiKey = "dQUFPMYVJJ1rAfF2seMYLq6ePtIjmf6pRJsuUG2pWN6g8p7eMJZI3DdA";

    fetch(`https://api.pexels.com/v1/search?query=${query}`, {
      headers: {
        Authorization: `Bearer ${apiKey}`
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
    .then(data => {
      const imageContainer = document.getElementById('imageContainer');
      imageContainer.innerHTML = '';

      data.photos.forEach(photo => {
        const img = document.createElement('img');
        img.src = photo.src.medium;
        img.alt = photo.photographer;

        const cardBody = document.createElement('div');
        cardBody.classList.add('card-body');

        const cardTitle = document.createElement('h5');
        cardTitle.textContent = photo.photographer;
        cardTitle.classList.add('card-title');

        const card = document.createElement('div');
        card.classList.add('card', 'mb-4', 'shadow-sm');
        card.appendChild(img);
        cardBody.appendChild(cardTitle);
        card.appendChild(cardBody);

        imageContainer.appendChild(card);
      });
    })
    .catch(error => {
      console.error('Si è verificato un errore:', error);
    });
  }


const editButtons = document.querySelectorAll('.btn-outline-secondary');
editButtons.forEach(button => {
  if (button.textContent.trim() === 'Edit') {
    button.textContent = 'Hide';
    button.classList.add('hide-button');
  }
});
document.querySelectorAll('.hide-button').forEach(button => {
  button.addEventListener('click', () => {
    const card = button.closest('.card');
    card.style.display = 'none';
  });
});
document.querySelectorAll('.card').forEach(card => {
  const timeElement = card.querySelector('.text-muted');
  const imageElement = card.querySelector('.card-img-top');
  const imageUrl = imageElement.src;
  const imageId = imageUrl.substring(imageUrl.lastIndexOf('/') + 1);
  timeElement.textContent = `ID: ${imageId}`;
});

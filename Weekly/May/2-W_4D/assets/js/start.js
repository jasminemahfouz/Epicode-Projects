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
      Authorization: `dQUFPMYVJJ1rAfF2seMYLq6ePtIjmf6pRJsuUG2pWN6g8p7eMJZI3DdA`
    }
  })
  .then((response) => {
      if (response.ok) {
        return response.json();
      } else {
        throw new Error('Errore nella risposta del server');
      }
    })
  .then(data => {
    const existingCards = document.querySelectorAll('.card');
    const imageContainer = document.getElementById('imageContainer');
    const newImages = data.photos.map(photo => {
      const img = document.createElement('img');
      img.src = photo.src.medium;
      img.alt = photo.photographer;
      return img;
    });

    existingCards.forEach((card, index) => {
      const newImage = newImages[index];
      if (newImage) {
        const oldImage = card.querySelector('.card-img-top');
        card.replaceChild(newImage, oldImage);
      }
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

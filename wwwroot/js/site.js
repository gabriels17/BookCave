// Write your JavaScript code.
const starTotal = 5;

var allStars = $('.thumbnail .stars-outer .stars-inner .ratings');

$.each(allStars, function (key, value) {
    var starValue = $(value).text();
    starValue = starValue.replace(",", ".");
    var starWidth = parseFloat(starValue) * 20;
    $('.stars-inner')[key].style.width = starWidth + "%";
});
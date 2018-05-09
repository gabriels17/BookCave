// Write your JavaScript code.
const starTotal = 5;

var allStars = $('.stars-outer .stars-inner .ratings');

$.each(allStars, function (key, value) {
    var starValue = $(value).text();
    starValue = starValue.replace(",", ".");
    var starWidth = parseFloat(starValue) * 20;
    $('.stars-inner')[key].style.width = starWidth + "%";
});

$(".thumbnail").delegate(".book-to-cart", 'click', function(){
    var booktitle = $(this).parent().parent().find(".book-title").text();
    var bookId = $(this).parent().parent().find(".img-resize").attr("alt");
    swal({
        title: booktitle,
        text: "has been added to the cart \n",
        icon: "success",
        buttons: "Okay",
      }).then((value) => {
        var url = "/Account/AddToCart/" + bookId; 
        window.location.href = url; 
      });
});
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

$(".details-book-to-cart").click(function() {
    var booktitle = $(".details-book-title").text();
    var bookId = $(".details-book-image").attr("alt");
    swal({
        title: booktitle,
        text: "has been added to the cart",
        icon: "success",
        buttons: "Okay",
      }).then((value) => {
        var url = "/Account/AddToCart/" + bookId; 
        window.location.href = url;
      });
})

$(".details-book-to-whishlist").click(function() {
    var booktitle = $(".details-book-title").text();
    var bookId = $(".details-book-image").attr("alt");
    swal({
        title: booktitle,
        text: "has been added to your wishlist!",
        icon: "success",
        buttons: "Okay",
      }).then((value) => {
        var url = "/Account/AddToWishlist/" + bookId; 
        window.location.href = url;
      });
})

$("#delete").click(function() {
    var deletebooktitle = $(this).parent().parent().parent().find(".details-book-title").text();
    var deletebookId = $(this).parent().parent().find(".details-book-image").attr("alt");
    console.log(deletebooktitle);
    console.log(deletebookId);
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover " + deletebooktitle,
        icon: "warning",
        buttons: true,
        dangerMode: true,
        })
        .then((willDelete) => {
        if (willDelete) {
            swal("Poof! " + deletebooktitle +  " has been deleted!", {
            icon: "success",
            }).then(() => {
                var url = "/Home/DeleteBook/" + deletebookId; 
                window.location.href = url;
            });
        } else {
            swal(booktitle + " is safe!");
        }
    });
});

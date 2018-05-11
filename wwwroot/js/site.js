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
    var booktitle = $(this).parent().parent().find(".book-title").text();   //This get's the books title
    var bookId = $(this).parent().parent().find(".img-resize").attr("alt"); //This gets the bookId
    swal({                                                                  //This is a sweet alert which is an extenstion to make pop-up windows better looking
        title: booktitle,
        text: "has been added to the cart \n",
        icon: "success",
        buttons: "Okay",
      }).then((value) => {                                                  //When the user pushes the okay button on the popup
        var url = "/Cart/AddToCart/" + bookId;                           //This will become the URL
        window.location.href = url;                                         //And redirects the user (there can be a easier or more secure way to do this but i didn't know how)
      });
});

$(".details-book-to-cart").click(function() {
    var booktitle = $(".details-book-title").text();                        //This does the same as above but for when you are inside book detail
    var bookId = $(".details-book-image").attr("alt");
    swal({
        title: booktitle,
        text: "has been added to the cart",
        icon: "success",
        buttons: "Okay",
      }).then((value) => {
        var url = "/Cart/AddToCart/" + bookId; 
        window.location.href = url;
      });
})

$(".details-book-to-whishlist").click(function() {                          //This is to add a book to wishlist and again with a better looking pop-up window
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

$("#delete").click(function() {                                                                       //This is to delete a book but first give the user a warning that he's about to delete a book
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

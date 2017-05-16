$(document).ready(
    function () {
        $('.reply').on("click", function () {
            console.log((this).children);
            $(this).children('.row').fadeToggle();
        });
    }
);
$(function () {
    $('.profile-description').readmore({
        collapsedHeight: 200,
        embedCSS: true,
        blockCSS: "display: inline;", 
        moreLink: '<a href="#" class="btn btn-default">...</a>',
        lessLink: '<a href="#" >Less</a>'
    });
});
// Write your Javascript code.
function addToCart(productId, quantity)
{
    $.ajax({
        type: 'post',
        url: '/cart/addtocart',
        data: {
            productId: productId,
            quantity: quantity
        },
        success: function (response) {
            if (response)
            {
                var url = window.location.href.toLowerCase().indexOf("cart");
                if (url > 0) {
                    location.reload();
                }
                else {
                    $.ajax({
                        type: 'get',
                        url: '/cart/refreshcart',
                        success: function (html) {

                            var container = $("#cartSummary");
                            container.html(html)
                            $("#cartNumber").text($("#cartTotalItem").val());
                        }
                    });
                }
            }
        }
    });
}
function removeFromCart(productId) {
    $.ajax({
        type: 'post',
        url: '/cart/removeFromCart',
        data: {
            productId: productId
        },
        success: function (response) {
            if (response) {
                var url = window.location.href.toLowerCase().indexOf("cart");
                if (url > 0)
                {
                    location.reload();
                }
                else
                {
                    $.ajax({
                        type: 'get',
                        url: '/cart/refreshcart',
                        success: function (html) {

                            var container = $("#cartSummary");
                            container.html(html)
                            $("#cartNumber").text($("#cartTotalItem").val());
                        }
                    });
                }
            }
        }
    });
}
function downCart(productId)
{
    $.ajax({
        type: 'post',
        url: '/cart/DownCart',
        data: {
            productId: productId
        },
        success: function (response) {
            if (response) {
                location.reload();
            }
        }
    });
}
function showOrder(order)
{
    $("#order-id").html(order.id);
    $("#order-date").html(order.date);
    $("#order-address").html(order.address);
    if(order.shipped)
    {
        $("#order-shipped").html("Đã giao");
    }
    else
    {
        $("#order-shipped").html("Chưa giao");
    }
}
function showOrderDetail(id)
{
    $.ajax({
        type: 'get',
        url: '/order/showorderdetail',
        data: {
            id: id
        },
        success: function (response) {
            $("#modalBody").html(response);
        }
    });
}

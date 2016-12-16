// Write your Javascript code.
function AddToCart(id, quantity)
{
    $.ajax({
        type: 'post',
        url: '/cart/addtocart',
        data: {
            id: id,
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
function RemoveFromCart(id) {
    $.ajax({
        type: 'post',
        url: '/cart/removeFromCart',
        data: {
            id: id
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
function DownCart(id)
{
    $.ajax({
        type: 'post',
        url: '/cart/DownCart',
        data: {
            id: id
        },
        success: function (response) {
            if (response) {
                location.reload();
            }
        }
    });
}
function ShowOrder(order)
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
function ShowOrderDetail(id)
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

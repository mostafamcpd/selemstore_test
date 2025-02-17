@section Scripts {
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#addToCartBtn').click(function (e) {
                e.preventDefault();

                var productId = $(this).data('product-id');
                var size = $(this).data('size');
                var color = $(this).data('color');
                var quantity = $(this).data('quantity');

                $.ajax({
                    type: 'POST',
                    url: '/Cart/AddToCart',
                    data: {
                        productId: productId,
                        size: size,
                        color: color,
                        quantity: quantity
                    },
                    success: function (response) {
                        if (response.success) {
                            showConfirmationModal(response.message);
                        } else {
                            alert(response.message);
                        }
                    },
                    error: function () {
                        alert('حدث خطأ أثناء إضافة المنتج. حاول مرة أخرى.');
                    }
                });
            });

            function showConfirmationModal(message) {
                var modalHtml = `
                    <div id="confirmationModal" style="position: fixed; top: 30%; left: 50%; transform: translate(-50%, -50%);
                        background-color: #fff; padding: 20px; border: 2px solid #ccc; z-index: 1000; text-align: center;">
                        <p>${message}</p>
                        <button id="continueShoppingBtn">متابعة التسوق</button>
                        <button id="goToCartBtn">الذهاب إلى سلة المشتريات</button>
                    </div>
                    <div id="modalOverlay" style="position: fixed; top: 0; left: 0; width: 100%; height: 100%;
                        background-color: rgba(0, 0, 0, 0.5); z-index: 999;"></div>
                `;

                $('body').append(modalHtml);

                $('#continueShoppingBtn').click(function () {
                    $('#confirmationModal, #modalOverlay').remove();
                });

                $('#goToCartBtn').click(function () {
                    window.location.href = '/Cart/Index';
                });
            }
        });
    </script>
}
// Добавление товара в корзину через AJAX (задание 4)
document.addEventListener('DOMContentLoaded', function () {
    var grid = document.getElementById('catalogGrid');

    if (!grid) {
        return;
    }

    // Делегирование событий на контейнер: после живого поиска (search.js)
    // карточки в #catalogGrid пересоздаются, и обычные addEventListener на
    // самих кнопках "потерялись" бы. Слушатель на постоянном родителе решает это.
    grid.addEventListener('click', function (event) {
        var button = event.target.closest('.add-to-cart');
        if (!button || button.disabled) {
            return;
        }

        var productId = button.dataset.productId;
        addToCart(productId, button);
    });

    async function addToCart(productId, button) {
        var originalText = button.textContent;

        // Заблокировать кнопку и показать спиннер, чтобы не отправить два запроса подряд
        button.disabled = true;
        button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> Добавление...';

        try {
            var response = await fetch('/Catalog/AddToCart', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                body: 'id=' + encodeURIComponent(productId)
            });

            if (!response.ok) {
                throw new Error('HTTP ' + response.status);
            }

            var data = await response.json();

            if (data.success) {
                // Обновить бейдж корзины в navbar
                var badge = document.getElementById('cartBadge');
                if (badge) {
                    badge.textContent = data.cartCount;
                }

                // Показать успех на кнопке
                button.innerHTML = 'Добавлено ✓';
                button.classList.remove('btn-primary');
                button.classList.add('btn-success');

                showCartToast(data.productName);

                // Через 2 секунды вернуть исходное состояние кнопки
                setTimeout(function () {
                    button.textContent = originalText;
                    button.classList.remove('btn-success');
                    button.classList.add('btn-primary');
                    button.disabled = false;
                }, 2000);
            } else {
                alert('Ошибка: ' + data.message);
                button.textContent = originalText;
                button.disabled = false;
            }
        } catch (error) {
            console.error(error);
            button.textContent = 'Ошибка';
            setTimeout(function () {
                button.textContent = originalText;
                button.disabled = false;
            }, 2000);
        }
    }

    // Toast-уведомление (доп. задание, лёгкий уровень)
    function showCartToast(productName) {
        var toastEl = document.getElementById('cartToast');
        var toastText = document.getElementById('cartToastText');
        if (!toastEl || !toastText || typeof bootstrap === 'undefined') {
            return;
        }

        toastText.textContent = 'Товар «' + productName + '» добавлен в корзину';
        var toast = bootstrap.Toast.getOrCreateInstance(toastEl, { delay: 3000 });
        toast.show();
    }
});

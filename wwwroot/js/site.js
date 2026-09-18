// Обновление счётчика корзины при загрузке любой страницы (задание 5)
document.addEventListener('DOMContentLoaded', async function () {
    var badge = document.getElementById('cartBadge');
    if (!badge) {
        return;
    }

    try {
        var response = await fetch('/Catalog/GetCartCount');
        var data = await response.json();
        badge.textContent = data.count > 0 ? data.count : '0';
    } catch (error) {
        console.error('Не удалось получить количество товаров в корзине:', error);
    }
});

// Автоматически скрывает alert через 4 секунды (задание 4 предыдущей практики)
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.alert').forEach(function (alertEl) {
        setTimeout(function () {
            var bsAlert = bootstrap.Alert.getOrCreateInstance(alertEl);
            bsAlert.close();
        }, 4000);
    });

    // Подсветка активного тега без перезагрузки страницы,
    // реальная фильтрация всё равно выполняется на сервере при переходе по ссылке
    // (доп. задание, средний уровень)
    var tagLinks = document.querySelectorAll('.tag-filter .tag-pill');
    tagLinks.forEach(function (link) {
        link.addEventListener('click', function () {
            tagLinks.forEach(function (l) {
                l.classList.remove('btn-primary');
                l.classList.add('btn-outline-secondary');
            });
            link.classList.remove('btn-outline-secondary');
            link.classList.add('btn-primary');
            // навигация происходит естественным образом через href/asp-action
        });
    });
});

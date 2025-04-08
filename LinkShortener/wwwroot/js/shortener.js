async function generateShortUrl() {
    const originalUrl = document.getElementById('originalUrl').value;
    const resultDiv = document.getElementById('result');

    // Валидация URL
    if (!originalUrl || !originalUrl.startsWith('http')) {
        resultDiv.innerHTML = '<div class="alert alert-danger">Введите корректный URL</div>';
        return;
    }

    try {
        const response = await fetch('/api/apishortener', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(originalUrl)
        });

        if (!response.ok) throw new Error(await response.text());

        const data = await response.json();

        resultDiv.innerHTML = `
            <div class="alert alert-success mt-3">
                <div class="alert alert-success mt-3">
                    Короткая ссылка:
                    <a href="${data.shortUrl}" id="shortUrlLink" target="_blank">${data.shortUrl}</a>
                    <button class="btn btn-sm btn-outline-secondary ms-2" onclick="copyShortUrl()">
                        📋 Копировать
                    </button>
                    <span id="copyStatus" class="text-success ms-2"></span>
                </div>
            </div>
        `;
    } catch (error) {
        resultDiv.innerHTML = `
            <div class="alert alert-danger mt-3">
                Ошибка: ${error.message}
            </div>
        `;
    }
}
async function copyShortUrl() {
    const shortUrl = document.getElementById('shortUrlLink').href;
    const statusElement = document.getElementById('copyStatus');

    try {
        await navigator.clipboard.writeText(shortUrl);
        statusElement.textContent = "Скопировано!";

        // Через 2 секунды убрать статус
        setTimeout(() => {
            statusElement.textContent = "";
        }, 2000);
    } catch (err) {
        console.error('Ошибка копирования:', err);
        statusElement.textContent = "Ошибка копирования";
    }
}
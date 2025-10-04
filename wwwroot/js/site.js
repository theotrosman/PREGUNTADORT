// ========== PreguntadOrt - JavaScript Interactivo ==========

$(document).ready(function() {
    // Inicializar funcionalidades
    initializeGame();
    initializeAnimations();
    initializeFormValidation();
    initializeAnswerSelection();
});

// ========== INICIALIZACIÓN DEL JUEGO ==========
function initializeGame() {
    console.log('🎮 PreguntadOrt iniciado correctamente');
    
    // Agregar efectos de carga a los contenedores
    $('.fadeInUp').each(function(index) {
        $(this).css('animation-delay', (index * 0.1) + 's');
    });
    
    // Efecto de pulso en el puntaje
    $('#puntaje').addClass('pulse');
}

// ========== ANIMACIONES Y EFECTOS VISUALES ==========
function initializeAnimations() {
    // Efecto hover en botones
    $('.btn-jugar, .btn-comenzar, .btn-responder, .btn-siguiente, .btn-volver, .btn-inicio').hover(
        function() {
            $(this).addClass('glow');
        },
        function() {
            $(this).removeClass('glow');
        }
    );
    
    // Efecto de entrada para elementos
    $(window).scroll(function() {
        $('.fadeInUp').each(function() {
            const elementTop = $(this).offset().top;
            const elementBottom = elementTop + $(this).outerHeight();
            const viewportTop = $(window).scrollTop();
            const viewportBottom = viewportTop + $(window).height();
            
            if (elementBottom > viewportTop && elementTop < viewportBottom) {
                $(this).addClass('fadeInUp');
            }
        });
    });
    
    // Efecto de rebote en el logo
    $('.logo').hover(function() {
        $(this).addClass('bounce');
        setTimeout(() => {
            $(this).removeClass('bounce');
        }, 1000);
    });
}

// ========== VALIDACIÓN DE FORMULARIOS ==========
function initializeFormValidation() {
    // Validación en tiempo real para el formulario de configuración
    $('#username').on('input', function() {
        const username = $(this).val().trim();
        if (username.length < 2) {
            $(this).addClass('error-flash');
            setTimeout(() => $(this).removeClass('error-flash'), 600);
        } else {
            $(this).addClass('success-flash');
            setTimeout(() => $(this).removeClass('success-flash'), 600);
        }
    });
    
    // Validación del formulario de configuración
    $('.form-configurar').on('submit', function(e) {
        const username = $('#username').val().trim();
        const dificultad = $('select[name="dificultad"]').val();
        const categoria = $('select[name="categoria"]').val();
        
        if (!username || username.length < 2) {
            e.preventDefault();
            showNotification('⚠️ Por favor ingresa un nombre de usuario válido', 'error');
            $('#username').focus().addClass('shake');
            setTimeout(() => $('#username').removeClass('shake'), 500);
            return false;
        }
        
        if (!dificultad) {
            e.preventDefault();
            showNotification('⚠️ Por favor selecciona una dificultad', 'error');
            $('select[name="dificultad"]').focus().addClass('shake');
            setTimeout(() => $('select[name="dificultad"]').removeClass('shake'), 500);
            return false;
        }
        
        if (!categoria) {
            e.preventDefault();
            showNotification('⚠️ Por favor selecciona una categoría', 'error');
            $('select[name="categoria"]').focus().addClass('shake');
            setTimeout(() => $('select[name="categoria"]').removeClass('shake'), 500);
            return false;
        }
        
        // Mostrar loading
        showLoading();
    });
}

// ========== SELECCIÓN DE RESPUESTAS ==========
function initializeAnswerSelection() {
    // Efecto de selección en las respuestas
    $('.respuesta-item').on('click', function() {
        // Remover selección anterior
        $('.respuesta-item').removeClass('selected');
        
        // Agregar selección actual
        $(this).addClass('selected');
        
        // Efecto visual
        $(this).addClass('pulse');
        setTimeout(() => {
            $(this).removeClass('pulse');
        }, 1000);
        
        // Habilitar botón de responder
        $('.btn-responder').prop('disabled', false);
    });
    
    // Validación del formulario de respuestas
    $('.respuestas-form').on('submit', function(e) {
        const selectedAnswer = $('input[name="idRespuesta"]:checked').val();
        
        if (!selectedAnswer) {
            e.preventDefault();
            showNotification('⚠️ Por favor selecciona una respuesta', 'error');
            $('.respuestas-grid').addClass('shake');
            setTimeout(() => $('.respuestas-grid').removeClass('shake'), 500);
            return false;
        }
        
        // Mostrar loading
        showLoading();
    });
}

// ========== NOTIFICACIONES ==========
function showNotification(message, type = 'info') {
    // Crear elemento de notificación
    const notification = $(`
        <div class="notification notification-${type}">
            <span>${message}</span>
            <button class="notification-close">&times;</button>
        </div>
    `);
    
    // Agregar estilos si no existen
    if (!$('#notification-styles').length) {
        $('head').append(`
            <style id="notification-styles">
                .notification {
                    position: fixed;
                    top: 20px;
                    right: 20px;
                    padding: 15px 20px;
                    border-radius: 8px;
                    color: white;
                    font-weight: 600;
                    z-index: 10000;
                    display: flex;
                    align-items: center;
                    gap: 10px;
                    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);
                    animation: slideInRight 0.3s ease-out;
                }
                .notification-info { background: linear-gradient(135deg, #3498db, #2980b9); }
                .notification-success { background: linear-gradient(135deg, #27ae60, #229954); }
                .notification-error { background: linear-gradient(135deg, #e74c3c, #c0392b); }
                .notification-warning { background: linear-gradient(135deg, #f39c12, #e67e22); }
                .notification-close {
                    background: none;
                    border: none;
                    color: white;
                    font-size: 18px;
                    cursor: pointer;
                    padding: 0;
                    width: 20px;
                    height: 20px;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                }
                @keyframes slideInRight {
                    from { transform: translateX(100%); opacity: 0; }
                    to { transform: translateX(0); opacity: 1; }
                }
                @keyframes slideOutRight {
                    from { transform: translateX(0); opacity: 1; }
                    to { transform: translateX(100%); opacity: 0; }
                }
            </style>
        `);
    }
    
    // Agregar al DOM
    $('body').append(notification);
    
    // Auto-remover después de 5 segundos
    setTimeout(() => {
        notification.css('animation', 'slideOutRight 0.3s ease-out');
        setTimeout(() => notification.remove(), 300);
    }, 5000);
    
    // Botón de cerrar
    notification.find('.notification-close').on('click', function() {
        notification.css('animation', 'slideOutRight 0.3s ease-out');
        setTimeout(() => notification.remove(), 300);
    });
}

// ========== LOADING ==========
function showLoading() {
    // Crear overlay de loading
    const loading = $(`
        <div id="loading-overlay">
            <div class="loading-spinner">
                <div class="spinner"></div>
                <p>Cargando...</p>
            </div>
        </div>
    `);
    
    // Agregar estilos si no existen
    if (!$('#loading-styles').length) {
        $('head').append(`
            <style id="loading-styles">
                #loading-overlay {
                    position: fixed;
                    top: 0;
                    left: 0;
                    width: 100%;
                    height: 100%;
                    background: rgba(0, 0, 0, 0.8);
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    z-index: 10000;
                }
                .loading-spinner {
                    text-align: center;
                    color: white;
                }
                .spinner {
                    width: 50px;
                    height: 50px;
                    border: 5px solid rgba(255, 255, 255, 0.3);
                    border-top: 5px solid #3498db;
                    border-radius: 50%;
                    animation: spin 1s linear infinite;
                    margin: 0 auto 20px;
                }
                @keyframes spin {
                    0% { transform: rotate(0deg); }
                    100% { transform: rotate(360deg); }
                }
            </style>
        `);
    }
    
    $('body').append(loading);
    
    // Remover después de 2 segundos (o cuando se complete la acción)
    setTimeout(() => {
        loading.fadeOut(300, function() {
            $(this).remove();
        });
    }, 2000);
}

// ========== EFECTOS ESPECIALES ==========
function addSpecialEffects() {
    // Efecto de confeti para respuestas correctas
    if (window.location.pathname.includes('Respuesta') && $('.resultado.correcto').length) {
        createConfetti();
    }
    
    // Efecto de celebración para el fin del juego
    if (window.location.pathname.includes('Fin')) {
        setTimeout(() => {
            createConfetti();
        }, 1000);
    }
}

// ========== CONFETI ==========
function createConfetti() {
    const colors = ['#3498db', '#e74c3c', '#f39c12', '#27ae60', '#9b59b6'];
    const confettiCount = 50;
    
    for (let i = 0; i < confettiCount; i++) {
        const confetti = $(`<div class="confetti"></div>`);
        confetti.css({
            position: 'fixed',
            width: '10px',
            height: '10px',
            backgroundColor: colors[Math.floor(Math.random() * colors.length)],
            left: Math.random() * 100 + '%',
            top: '-10px',
            zIndex: 10000,
            animation: `confettiFall ${2 + Math.random() * 3}s linear forwards`
        });
        
        $('body').append(confetti);
        
        setTimeout(() => confetti.remove(), 5000);
    }
    
    // Agregar estilos de confeti si no existen
    if (!$('#confetti-styles').length) {
        $('head').append(`
            <style id="confetti-styles">
                @keyframes confettiFall {
                    to {
                        transform: translateY(100vh) rotate(720deg);
                        opacity: 0;
                    }
                }
            </style>
        `);
    }
}

// ========== INICIALIZAR EFECTOS ESPECIALES ==========
$(document).ready(function() {
    addSpecialEffects();
});

// ========== UTILIDADES ==========
function formatTime(seconds) {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
}

function getRandomColor() {
    const colors = ['#3498db', '#e74c3c', '#f39c12', '#27ae60', '#9b59b6', '#1abc9c'];
    return colors[Math.floor(Math.random() * colors.length)];
}

// ========== DEBUGGING ==========
if (window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1') {
    console.log('🎮 PreguntadOrt - Modo desarrollo activado');
    console.log('📊 Funciones disponibles:', {
        showNotification: 'showNotification(message, type)',
        showLoading: 'showLoading()',
        createConfetti: 'createConfetti()',
        formatTime: 'formatTime(seconds)',
        getRandomColor: 'getRandomColor()'
    });
}
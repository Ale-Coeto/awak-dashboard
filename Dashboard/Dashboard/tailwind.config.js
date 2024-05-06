/** @type {import('tailwindcss').Config} */

module.exports = {
    content: [
        './**/*.{razor,html,cshtml}',
        './wwwroot/**/*.js',
    ],
    plugins: [
        require('@tailwindcss/forms'),
        require('@tailwindcss/aspect-ratio'),
        require('@tailwindcss/typography'),
    ],
    theme: {
        extend: {
            colors: {
                'verde-claro': '#89B486',
                'verde-principal': '#2C5629',
                'verde-oscuro': '#0C270A',
                'verde-hover': '#AFDFAB',
                'verde-texto': '#407B46',
                'gris-oscuro': '#4E4F5D',
                'gris-claro': '#F1F2F7',
                'gris-select': '#E4E7F4',
                'amarillo': '#F9B235',
                'rojo': '#C75A4D',
            },
            //fontFamily: {
            //    'inter': 'Inter',
            //    'jersey': 'Jersey 25 Charted',
            //    'sans': ['Roboto']
            //}
        }
    }
}
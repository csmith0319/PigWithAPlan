/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      colors: {
        primary: "#6B60E9",
      },
      fontFamily: {
        sans: ["Jost", "sans-serif"],
      },
    },
  },
  plugins: [],
};

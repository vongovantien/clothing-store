export function toCurrency(number) {
    console.log(number)
    const formatter = new Intl.NumberFormat('en-IN', { maximumSignificantDigits: 3 })

    return formatter.format(number);
}
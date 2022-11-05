import axios from "axios"

export let endpoint = {
    product: (numberPage, pageSize) => `Product?PageNumber=${numberPage}&PageSize=${pageSize}`,
    productDetail: (id) => `Product/${id}`,
    category: "Category",
    //menu: "Menu"
}

export default axios.create({
    baseURL: `https://localhost:44340/api/`,
    headers: {
        'Content-Type': 'application/json'
    }
})
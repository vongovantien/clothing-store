import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import productService from "services/product.service";

const initialState = {
    products: [],
};


export const getAllProduct = createAsyncThunk(
    "products/GetAll",
    async () => {
        const res = await productService.getAll();
        return res.data;
    }
);

export const createProduct = createAsyncThunk(
    "tutorials/CreateProduct",
    async (data) => {
        const res = await productService.create(data);
        return res.data;
    }
);

export const getProductByID = createAsyncThunk(
    "products/GetByID",
    async () => {
        const res = await productService.getProductByID();
        return res.data;
    }
);

export const updateProduct = createAsyncThunk(
    "products/Update",
    async () => {
        const res = await productService.update();
        return res.data;
    }
);

export const deleteProduct = createAsyncThunk(
    "products/Delete",
    async () => {
        const res = await productService.delete();
        return res.data;
    }
);

export const categorySlice = createSlice({
    name: 'categories',
    initialState,
    extraReducers: {
        [getAllProduct.fulfilled]: (state, action) => {
            return [...action.payload];
        },
        [createProduct.fulfilled]: (state, action) => {
            console.log(state)
            state.products.push(action.payload);
        },
        [updateProduct.fulfilled]: (state, action) => {
            const index = state.findIndex(tutorial => tutorial.id === action.payload.id);
            state[index] = {
                ...state[index],
                ...action.payload,
            };
        },
        [deleteProduct.fulfilled]: (state, action) => {
            let index = state.findIndex(({ id }) => id === action.payload.id);
            state.splice(index, 1);
        },
    }
})


export default categorySlice.reducer;

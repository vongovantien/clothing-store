import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import productService from "../../services/product.service";

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
    "products/CreateProduct",
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

export const getHotProduct = createAsyncThunk(
    "products/GetHotProduct",
    async (pageSize, pageNumber) => {
        console.log(pageSize)
        console.log(pageNumber)
        const res = await productService.getHotProduct(pageSize, pageNumber);
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

export const productSlice = createSlice({
    name: 'products',
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
        [deleteProduct.fulfilled]: (state, action) => {
            return [];
        },
        // [findTutorialsByTitle.fulfilled]: (state, action) => {
        //     return [...action.payload];
        // },
    }
})


const { reducer } = productSlice;
export default reducer;

// export const { addProduct } = productSlice.actions;
// export const selectProduct = (state) => state.products;
// export default productSlice.reducer;
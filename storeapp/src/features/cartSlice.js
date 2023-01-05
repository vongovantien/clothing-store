import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axios } from 'axios';

const initialState = {
    cartItems: [],
    amount: 0,
    total: 0,
    isLoading: true
};

const url = '';

export const getCartItems = createAsyncThunk('cart/getCartItems', async (_, thunkAPI) => {
    try {
        const res = await axios(url);
        return res.data;
    } catch (error) {
        console.log(error)
        thunkAPI.rejectWithValue("error")
    }
})


export const cartSlice = createSlice({
    name: 'cart',
    initialState,
    reducers: {
        clearCart: (state) => {
            state.cartItems = [];
        },
        removeItem: (state, action) => {
            const itemId = action.payload;
            state.cartItems = state.cartItems.filter(item => item.id !== itemId);
        },
        increaseItem: (state, { payload }) => {
            const cartItem = state.cartItems.find(item => item.id === payload.id);
            cartItem.amount = cartItem.amount + 1;
        },
        decreaseItem: (state, { payload }) => {
            const cartItem = state.cartItems.find(item => item.id === payload.id);
            cartItem.amount = cartItem.amount - 1;
        },
        calculateTotals: (state) => {
            let amount = 0;
            let total = 0;
            state.cartItems.forEach(item => {
                amount += item.about;
                total += item.amount * item.price;
            })
            state.amount = amount;
            state.total = total;
        }
    },
    extraReducers: {
        [getCartItems.pending]: (state) => {
            state.isLoading = true;
        },
        [getCartItems.fulfilled]: (state, action) => {
            console.log(action)
            state.isLoading = true;
            state.cartItems = action.payload;
        },
        [getCartItems.rejected]: state => {
            state.isLoading = false;
        }
    }

})

export const { clearCart, removeItem, increaseItem, decreaseItem, calculateTotals } = cartSlice.actions;
export default cartSlice.reducer;
import { configureStore } from '@reduxjs/toolkit';
import productSlice from 'features/products/productSlice';

const reducer = {
    products: productSlice
}

const store = configureStore({
    reducer: reducer,
    devTools: true,
})

export default store;
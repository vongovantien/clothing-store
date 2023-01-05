import { configureStore } from '@reduxjs/toolkit';
import productReducer from 'features/productSlice';
import categoryReducer from 'features/categorySlice';
import authReducer from 'features/authSlice';

const reducer = {
    product: productReducer,
    category: categoryReducer,
    user: authReducer
}

const store = configureStore({
    reducer: reducer,
})

export default store;
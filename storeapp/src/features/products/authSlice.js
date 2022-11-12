import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    isLoading: false,
    errorMessage: '',
    currentUser: null,
};

export const productSlice = createSlice({
    name: 'user',
    initialState,
    extraReducers: {
        // [deleteAllTutorials.fulfilled]: (state, action) => {
        //     return [];
        // },
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
import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import authService from 'services/auth.service';

const initialState = {
    data: {},
    loading: false,
    error: null,
};

export const authentication = createAsyncThunk(
    "users/authenticate",
    async (data) => {
        const res = await authService.authentication(data);
        return res.data;
    }
);

export const authSlice = createSlice({
    name: 'authentication',
    initialState,
    reducers: {
        logout: (state) => {
            state.user = {};
            state.error = null;
            state.loading = false;
        },
    },
    extraReducers: {
        [authentication.pending]: (state) => {
            state.loading = true
        },
        [authentication.fulfilled]: (state, { payload }) => {
            state.data = payload
            localStorage.setItem('user', payload)
            state.loading = false;
        },
        [authentication.rejected]: (state) => {
            state.loading = false
            state.error = 'Lỗi xảy ra, xin vui lòng thử lại sau !!'
        }
    }
})
export const { logout } = authSlice.actions
export default authSlice.reducer;
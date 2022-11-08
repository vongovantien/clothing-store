import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, Grid, InputLabel, MenuItem, Paper, Select, TextField, Typography } from '@mui/material';
import { Box } from '@mui/system';
import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { createProduct } from '../../../features/products/productSlice';
import categoryService from '../../../services/category.service';

const FormAddProduct = ({ show, handleOpenForm }) => {
    const [categoryList, setCategoryList] = useState([]);

    const [productName, setProductName] = useState("");
    const [productPrice, setProductPrice] = useState("");
    const [productCode, setProductCode] = useState("");
    const [quantity, setQuantity] = useState("");
    const [description, setDescription] = useState("");
    const [discount, setDiscount] = useState("");
    const [category, setCategory] = useState("");

    const dispatch = useDispatch();

    useEffect(() => {
        try {
            getAllCategory();
        } catch (error) {
            console.log(error);
        }
    }, [])

    const getAllCategory = async () => {
        let res = await categoryService.getAll()
        setCategoryList(res.data)
    }

    const onSubmitForm = (event) => {
        event.preventDefault();
        const formData = {
            title: productName,
            categoryId: category,
            price: productPrice,
            code: productCode,
            description,
            discount,
            quantity
        }
        dispatch(createProduct(formData))
            .unwrap()
            .then((res) => {
                console.log(res)
                if (res.status === 200) {
                    handleOpenForm(!show)
                }
            })
            .catch(res => console.error(res))
    }


    return (
        <div>
            <Dialog disablePortal maxWidth='md' fullWidth open={show}>
                <DialogTitle>Thêm mới sản phẩm</DialogTitle>
                <DialogContent>
                    <Paper>
                        <Box px={3} py={2}>
                            <Grid container spacing={1}>
                                <Grid item xs={12}>
                                    <TextField size="small" id="outlined-basic" fullWidth label="Tên sản phẩm" onChange={e => setProductName(e.target.value)}
                                        variant="outlined" type="text" />
                                    <Typography variant="filled" color="textSecondary">

                                    </Typography>
                                </Grid>
                                <Grid item xs={12}>
                                    <TextField size="small" id="outlined-basic" fullWidth label="Giá sản phẩm" variant="outlined" type="number" onChange={e => setProductPrice(e.target.value)} />
                                    <Typography variant="filled" color="textSecondary">

                                    </Typography>
                                </Grid>
                                <Grid item xs={12} sm={12}>
                                    <TextField fullWidth size="small" id="outlined-basic" label="Mã sản phẩm" variant="outlined" type="text" onChange={e => setProductCode(e.target.value)} />
                                    <Typography variant="inherit" color="textSecondary">

                                    </Typography>
                                </Grid>
                                <Grid item xs={12} sm={12}>
                                    <TextField fullWidth size="small" id="outlined-basic" label="Giảm giá" variant="outlined" type="email" onChange={e => setDiscount(e.target.value)} />
                                    <Typography variant="inherit" color="textSecondary">

                                    </Typography>
                                </Grid>
                                <Grid item xs={12} sm={12}>
                                    <TextField fullWidth size="small" id="outlined-basic" label="Số lượng" variant="outlined" type="number" onChange={e => setQuantity(e.target.value)} />
                                    <Typography variant="inherit" color="textSecondary">

                                    </Typography>
                                </Grid>
                                <Grid item xs={12} sm={12}>
                                    <FormControl sx={{ minWidth: 220 }} fullWidth size="small">
                                        <InputLabel id="demo-select-small">Loại sản phẩm</InputLabel>
                                        <Select
                                            labelId="demo-select-small"
                                            id="demo-simple-select"
                                            value={category}
                                            label="Age"
                                            onChange={e => setCategory(e.target.value)}
                                        >
                                            {!!categoryList && categoryList.map((item, index) =>
                                                <MenuItem key={index} value={item.id}>{item.name}</MenuItem>
                                            )}
                                        </Select>
                                    </FormControl>
                                </Grid>
                                <Grid item xs={12} sm={12}>
                                    <TextField fullWidth size="small"
                                        id="outlined-multiline-static"
                                        label="Description"
                                        multiline
                                        rows={4}
                                        placeholder="Enter your description"
                                        variant="outlined"
                                        onChange={e => setDescription(e.target.value)}
                                    />
                                </Grid>
                            </Grid>
                        </Box>
                    </Paper>
                </DialogContent>
                <DialogActions>
                    <Button type="submit" variant="contained" color="primary" onClick={onSubmitForm}>
                        Save
                    </Button>
                    <Button onClick={() => handleOpenForm(!show)}>Cancel</Button>
                </DialogActions>
            </Dialog>
        </div >
    );
}

export default FormAddProduct
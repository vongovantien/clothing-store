import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, FormHelperText, Grid, InputLabel, MenuItem, Select, TextField } from '@mui/material';
import { useFormik } from 'formik';
import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { toast } from 'react-toastify';
import categoryService from 'services/category.service';
import * as yup from 'yup';

const validationSchema = yup.object({
    title: yup
        .string('Enter your title')
        .required('Title is required'),
    price: yup
        .string('Enter your price')
        .required('Password is required'),
    code: yup
        .string('Enter your code')
        .min(8, 'Code should be of minimum 8 characters length')
        .required('Password is required'),
});


export default function FormAddProduct(props) {
    const formik = useFormik({
        initialValues: {
            title: "",
            categoryId: "",
            price: "",
            code: "",
            description: "",
        },
        validationSchema: validationSchema,
        onSubmit: (values) => {
            createProduct(values)
        },
    });
    const [categoryList, setCategoryList] = useState([]);
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

    const createProduct = (event) => {
        event.preventDefault()
        dispatch(createProduct())
            .unwrap()
            .then((res) => {
                console.log(res)
                if (res.code === 200) {
                    toast.success("Add Product Successfully !!", {
                        position: "bottom-right",
                        hideProgressBar: false,
                        closeOnClick: true,
                        pauseOnHover: true,
                        draggable: true,
                        progress: undefined,
                        theme: "light",
                        autoClose: 5000,
                    });
                    props.handleOpenForm(!props.show)
                }
            })
            .catch(res => toast.error("Something wrong !!", {
                position: "bottom-right",
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "light",
                autoClose: 5000,
            }))
    }


    return (
        <div>
            <Dialog disablePortal maxWidth='md' fullWidth open={props.show}>
                <DialogTitle>Thêm mới sản phẩm</DialogTitle>
                <form onSubmit={formik.handleSubmit}>
                    <DialogContent>
                        <Grid container spacing={2}>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    fullWidth
                                    id="title"
                                    name="title"
                                    label="Title"
                                    size="small"
                                    value={formik.values.title}
                                    onChange={formik.handleChange}
                                    error={formik.touched.title && Boolean(formik.errors.title)}
                                    helperText={formik.touched.title && formik.errors.title}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <FormControl fullWidth >
                                    <InputLabel id="category">Category</InputLabel>
                                    <Select
                                        labelId="category"
                                        id="categoryId"
                                        size="small"
                                        label="categoryId"
                                        name="categoryId"
                                        value={formik.categoryId}
                                        onChange={formik.handleChange}
                                    >
                                        {!!categoryList && categoryList.map((item, index) => <MenuItem key={index} value={item.id}>{item.name}</MenuItem>)}
                                    </Select>
                                    <FormHelperText>{(formik.errors.categoryId && formik.touched.categoryId) && formik.errors.categoryId}</FormHelperText>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    fullWidth
                                    id="price"
                                    name="price"
                                    label="Price"
                                    type="number"
                                    size="small"
                                    value={formik.values.price}
                                    onChange={formik.handleChange}
                                    error={formik.touched.price && Boolean(formik.errors.price)}
                                    helperText={formik.touched.price && formik.errors.price}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    fullWidth
                                    id="code"
                                    name="code"
                                    label="Code"
                                    size="small"
                                    value={formik.values.code}
                                    onChange={formik.handleChange}
                                    error={formik.touched.code && Boolean(formik.errors.code)}
                                    helperText={formik.touched.code && formik.errors.code}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    fullWidth
                                    id="description"
                                    name="description"
                                    label="Description"
                                    type="text"
                                    size="small"
                                    value={formik.values.description}
                                    onChange={formik.handleChange}
                                    error={formik.touched.description && Boolean(formik.errors.description)}
                                    helperText={formik.touched.description && formik.errors.description}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    fullWidth
                                    id="image"
                                    name="image"
                                    type="file"
                                    size="small"
                                    onChange={formik.handleChange}
                                />
                            </Grid>
                        </Grid>
                    </DialogContent>
                    <DialogActions>
                        <Button type="submit" variant="contained" color="primary">
                            Save
                        </Button>
                        <Button onClick={() => props.handleOpenForm(!props.show)}>Cancel</Button>
                    </DialogActions>
                </form>
            </Dialog >
        </div >
    );
}
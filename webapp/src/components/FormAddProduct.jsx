import { createProduct } from '@features/products/productSlice';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, Grid, TextField } from '@mui/material';
import { useFormik } from 'formik';
import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { toast } from 'react-toastify';
import * as yup from 'yup';
import categoryService from '../../services/category.service';

const validationSchema = yup.object({
    email: yup
        .string('Enter your email')
        .email('Enter a valid email')
        .required('Email is required'),
    password: yup
        .string('Enter your password')
        .min(8, 'Password should be of minimum 8 characters length')
        .required('Password is required'),
});

const FormAddProduct = ({ show, handleOpenForm }) => {
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
            console.log(values)
        },
    });
    const [categoryList, setCategoryList] = useState([]);

    // const [productName, setProductName] = useState("");
    // const [productPrice, setProductPrice] = useState("");
    // const [productCode, setProductCode] = useState("");
    // const [quantity, setQuantity] = useState("");
    // const [description, setDescription] = useState("");
    // const [discount, setDiscount] = useState("");
    // const [category, setCategory] = useState("");

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
                    handleOpenForm(!show)

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
            <Dialog disablePortal maxWidth='md' fullWidth open={show}>
                <DialogTitle>Thêm mới sản phẩm</DialogTitle>
                <form onSubmit={formik.handleSubmit}>
                    <DialogContent>
                        <FormControl style={{ justifyContent: "center" }} fullWidth variant="outlined">
                            <Grid container spacing={1}>
                                <Grid item xs={12}>
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
                                    <TextField style={{ marginTop: "20px" }}
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
                                    <TextField style={{ marginTop: "20px" }}
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
                                    <TextField style={{ marginTop: "20px" }}
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
                            </Grid>
                        </FormControl>
                    </DialogContent>
                    <DialogActions>
                        <Button type="submit" variant="contained" color="primary">
                            Save
                        </Button>
                        <Button onClick={() => handleOpenForm(!show)}>Cancel</Button>
                    </DialogActions>
                </form>
            </Dialog >
        </div >
    );
}

export default FormAddProduct
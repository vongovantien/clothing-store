import { Button, Container, FormControl, TextField } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import * as yup from 'yup';

const validationSchema = yup.object({
    email: yup
        .string('Enter your email')
        .email('Enter a valid email')
        .required('Email is required'),
    password: yup
        .string('Enter your password')
        .min(8, 'Password should be of minimum 8 characters length')
        .required('Password is required'),
    confirmPassword: yup
        .string('Enter your password')
        .min(8, 'Password should be of minimum 8 characters length')
        .required('Password is required')
        .oneOf([yup.ref("password"), null], "Password is not match")
});
export const SignUp = () => {

    const formik = useFormik({
        initialValues: {
            email: '',
            password: '',
            confirmPassword: ''
        },
        validationSchema: validationSchema,
        onSubmit: (values) => {
            console.log(values)
        },
    });

    return (
        <Container style={{ paddingTop: "20px" }} maxWidth="sm">
            <h1 style={{ textAlign: "center" }}>Đăng ký</h1>
            <form onSubmit={formik.handleSubmit}>
                <FormControl style={{ justifyContent: "center" }} fullWidth variant="outlined">
                    <TextField style={{ marginTop: "20px" }}
                        id="email"
                        name="email"
                        label="Email"
                        type="text"
                        fullWidth
                        value={formik.values.email}
                        onChange={formik.handleChange}
                        error={formik.touched.email && Boolean(formik.errors.email)}
                        helperText={formik.touched.email && formik.errors.email}
                    />
                    <TextField style={{ marginTop: "20px" }}
                        id="password"
                        name="password"
                        label="Password"
                        type="password"
                        fullWidth
                        value={formik.values.password}
                        onChange={formik.handleChange}
                        error={formik.touched.password && Boolean(formik.errors.password)}
                        helperText={formik.touched.password && formik.errors.password}
                    />
                    <TextField style={{ marginTop: "20px" }}
                        id="confirmPassword"
                        name="confirmPassword"
                        label="Confirm Password"
                        type="password"
                        fullWidth
                        value={formik.values.confirmPassword}
                        onChange={formik.handleChange}
                        error={formik.touched.confirmPassword && Boolean(formik.errors.confirmPassword)}
                        helperText={formik.touched.confirmPassword && formik.errors.confirmPassword}
                    />
                    <Button color="primary" variant="contained" fullWidth type="submit" style={{ marginTop: "20px" }}>
                        Submit
                    </Button>
                </FormControl>
            </form>
        </Container>
    )
}

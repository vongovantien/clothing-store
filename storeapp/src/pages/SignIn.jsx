import { yupResolver } from '@hookform/resolvers/yup';
import { Button, Container, FormControl, TextField } from '@mui/material';
import { authentication } from 'features/authSlice';
import React, { useEffect, useState } from 'react';
import { useAuthState } from 'react-firebase-hooks/auth';
import { useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import * as yup from 'yup';

import { auth, signInWithFacebook, signInWithGoogle } from '../firebaseConfig';

const validationSchema = yup.object({
    email: yup.string()
        .required('Email is required')
        .email('Email is invalid'),
    password: yup.string()
        .required('Password is required')
        .min(6, 'Password must be at least 6 characters')
        .max(40, 'Password must not exceed 40 characters'),
});

export const SignIn = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    //const { data, loading, error } = useSelector((state) => state.user)
    // const { register, control, handleSubmit, formState: { errors } } = useForm({
    //     mode: 'onChange',
    //     resolver: yupResolver(validationSchema),
    // });
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [name, setName] = useState("");
    const [user, loading, error] = useAuthState(auth);

    useEffect(() => {
        if (loading) return;
        console.log(user)
        if (user) {
            navigate("/")
            toast.success("Log in successfully!!");
        }
    }, [user, loading]);

    const submitForm = async (data) => {
        //dispatch(authentication(data));
        //if (Object.keys(data).length !== 0) { navigate("/") };
    };

    return (
        <div>
            <Container style={{ paddingTop: "20px" }} maxWidth="sm">
                <h1 style={{ textAlign: "center" }}>Đăng nhập</h1>
                {/* <form onSubmit={handleSubmit(submitForm)}>
                    <FormControl style={{ justifyContent: "center" }} fullWidth variant="outlined">
                        <TextField
                            required
                            fullWidth
                            id="email"
                            name="email"
                            label="Email"
                            {...register('email')}
                            error={errors.email ? true : false}
                            helperText={errors.email?.message}
                        />
                        <TextField style={{ marginTop: "20px" }}
                            fullWidth
                            required
                            id="password"
                            name="password"
                            label="Password"
                            type="password"
                            {...register('password')}
                            error={errors.password ? true : false}
                            helperText={errors.password?.message}
                        />
                        <Button color="primary" variant="contained" fullWidth type="submit" style={{ marginTop: "20px" }}>
                            Submit
                        </Button>
                    </FormControl>
                </form> */}
            </Container>

            <button className="button" onClick={signInWithGoogle}><i className="fab fa-google"></i>Sign in with google</button>
            <button className="button" onClick={signInWithFacebook}><i className="fab fa-facebook"></i>Sign in with facebook</button>
        </div>
    );
};

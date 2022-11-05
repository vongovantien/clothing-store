import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import ImportExportIcon from '@mui/icons-material/ImportExport';
import { Button, Container, Stack } from '@mui/material';
import React, { useEffect, useState } from 'react';
import productService from '../../../services/product.service';
import FormAddProduct from '../components/FormAddProduct';



const ProductPage = () => {
    const [show, setShow] = useState(false)
    const [productList, setProductList] = useState([])
    useEffect(() => {
        getProductList();
    }, [])

    const getProductList = async () => {
        try {
            let products = await productService.getAll();
            console.log(products)
            setProductList(products.data)
        } catch (error) {
            console.log(error);
        }
    }

    const handleOpenForm = () => {
        setShow(!show);
    }

    return (
        <div>
            <FormAddProduct show={show} handleOpenForm={handleOpenForm} />
            <Stack direction="row" alignItems="center" justifyContent="flex-end" spacing={2} mr={2}>
                <Button variant="contained" startIcon={<AddCircleOutlineIcon />} onClick={() => handleOpenForm()}>Thêm mới sản phẩm</Button>
                <Button variant="contained" startIcon={<ImportExportIcon />}>Import</Button>
                <Button variant="contained">Export</Button>
            </Stack>
            <h1 style={{ textAlign: "center" }}>Danh sách sản phẩm</h1>
            <Container>
                {/* <CustomPaginationActionsTable props={productList} /> */}
            </Container>
        </div >
    )
}

export default ProductPage

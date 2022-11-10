import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import ImportExportIcon from '@mui/icons-material/ImportExport';
import { Button, Container, Stack } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import StickyHeadTable from '../../../components/StickyHeadTable';
import { ProductConstants } from '../../../utils/constants/ProductContants';
import FormAddProduct from '../components/FormAddProduct';


const columns = [
    { id: 'action', label: 'Hành động', minWidth: 170 },
    { id: 'code', label: 'Mã sản phẩm', minWidth: 170 },
    { id: 'title', label: 'Tên sản phẩm', minWidth: 170 },
    { id: 'price', label: 'Giá sản phầm', minWidth: 100 },
    { id: 'categoryId', label: 'Loại sản phẩm', minWidth: 170 }
];


const ProductPage = () => {
    const [show, setShow] = useState(false)
    const [productList, setProductList] = useState([])
    const [pageSize, setPageSize] = useState(5)
    const [pageNumber, setPageNumber] = useState(0)
    const dispatch = useDispatch()
    useEffect(() => {
        getProductList();
    }, [])

    const getProductList = async () => {
        try {
            let products = dispatch(getProductPaging)
            console.log(products)
            setProductList(products.data)
        } catch (error) {
            console.log(error);
        }
    }

    const handleOpenForm = () => {
        setShow(!show);
    }

    const handlePaging = (value, type) => {
        switch (type) {
            case ProductConstants.PAGE_SIZE:
                setPageSize(value)
                break;
            case ProductConstants.PAGE_NUMBER:
                setPageNumber(value)
                break;
            default:
                break;
        }
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
                <StickyHeadTable columns={columns} rows={productList} pageSize={pageSize} pageNumber={pageNumber} handlePaging={handlePaging} />
            </Container>
        </div >
    )
}

export default ProductPage

import { TabContext, TabList, TabPanel } from '@mui/lab';
import { Box, CircularProgress, Container, Grid, Pagination, Tab } from '@mui/material';
import CustomArrows from 'components/Carousel';
import ItemCard from 'components/ItemCard';
import { getHotProduct } from 'features/products/productSlice';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

export const Home = () => {
	const [productByTypeList, setProductByTypeList] = useState([]);
	const dispatch = useDispatch();
	const [pageSize, setPageSize] = useState(4)
	const [pageNumber, setPageNumber] = useState(1)

	const { loading, error, products } = useSelector((state) => state.products);
	//tab
	const [productType, setProductType] = useState('sale-product');

	const handleChange = (event, newValue) => {
		setProductType(newValue);
	};

	useEffect(() => {
		dispatch(getHotProduct({ pageSize, pageNumber }))
	}, [])

	useEffect(() => {
		getProductByType();
	}, [])

	const getProductByType = async () => {
		dispatch(getHotProduct({ pageSize, pageNumber }))
			.unwrap()
			.then((res) => {
				setProductByTypeList(res.data)
			})
			.catch(res => console.error(res))
	};
	const onHandlePaging = (evt, value) => {
		setPageNumber(value)
	}

	return (
		<>
			<Container maxWidth="xl">
				<Container>
					<CustomArrows />
				</Container>
				<Grid container spacing={2}>
					<Grid item xs={12}>
						<h1 style={{ textAlign: "center" }}>Top sản phẩm bán chạy trong tháng</h1>
						<Grid
							container
							spacing={4}
							justifyItems="center"
							style={{ marginTop: "80px" }}
						>

							{loading ? <CircularProgress /> : !!products.data && products.data.map(item =>
								<Grid key={item.id} item xs={12} sm={6} md={3}>
									<ItemCard prop={item} />
								</Grid>
							)}
						</Grid>
					</Grid>
					<Pagination count={products.totalPages} color="primary" style={{ padding: "20px 0", margin: "0 auto" }} onChange={onHandlePaging} />
				</Grid>
				<Box sx={{ width: '100%', typography: 'body1' }}>
					<TabContext value={productType}>
						<Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
							<TabList onChange={handleChange} aria-label="lab API tabs example">
								<Tab label="New Product" value="new-product" />
								<Tab label="OnSale" value="sale-product" />
								<Tab label="Feature Product" value="feature-product" />
							</TabList>
						</Box>
						<TabPanel value="new-product">
							<Grid container spacing={2}>
								<Grid item xs={12}>
									<Grid
										container
										spacing={4}
										justifyItems="center"
										style={{ marginTop: "10px" }}
									>
										{!!productByTypeList.data && productByTypeList.data.map(item =>
											<Grid key={item.id} item xs={12} sm={6} md={3}>
												<ItemCard prop={item} />
											</Grid>
										)}
									</Grid>
								</Grid>
							</Grid>
						</TabPanel>
						<TabPanel value="sale-product"><Grid container spacing={2}>
							<Grid item xs={12}><Grid
								container
								spacing={4}
								justifyItems="center"
								style={{ marginTop: "10px" }}
							>
								{!!productByTypeList.data && productByTypeList.data.map(item =>
									<Grid key={item.id} item xs={12} sm={6} md={3}>
										<ItemCard prop={item} />
									</Grid>
								)}
							</Grid>
							</Grid>
						</Grid>
						</TabPanel>
						<TabPanel value="feature-product">
							<Grid container spacing={2}>
								<Grid item xs={12}>
									<Grid
										container
										spacing={4}
										justifyItems="center"
										style={{ marginTop: "10px" }}
									>
										{!!productByTypeList.data && productByTypeList.data.map(item =>
											<Grid key={item.id} item xs={12} sm={6} md={3}>
												<ItemCard prop={item} />
											</Grid>
										)}
									</Grid>
								</Grid>
							</Grid>
						</TabPanel>
					</TabContext>
				</Box>
			</Container>
		</>
	)
}

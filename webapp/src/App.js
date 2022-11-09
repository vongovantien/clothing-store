import { Box, CircularProgress } from '@mui/material';
import { lazy, Suspense } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import "slick-carousel/slick/slick-theme.css";
import "slick-carousel/slick/slick.css";
import './App.css';
import NavBar from './components/NavBar';
import About from './pages/About';
import { DashBoard } from './pages/admin/DashBoard';
import Contact from './pages/Contact';
import { Home } from './pages/Home';
import NotFound from './pages/NotFound';
import Product from './pages/Product';
import ProductDetail from './pages/ProductDetail';
import { SignUp } from './pages/SignUp';

const ProductPage = lazy(() => import('./pages/admin/page/ProductPage'));
function App() {
  //useGaTracker();
  return (
    <>
      <ToastContainer />
      <BrowserRouter>
        <NavBar />
        <Suspense
          fallback={(
            <p className='fixed left-1/2 top-1/2 -translate-x-1/2 text-2xl'>
              Loading ...
            </p>
          )}
        >
          <Routes>
            <Route path="" element={<Home />} />
            <Route path="sign-up" element={< SignUp />} />
            <Route path="about" element={<About />} />
            <Route path="contact" element={<Contact />} />
            <Route path="product" element={<Product />} />
            <Route path="product/:id" element={<ProductDetail />} />
            <Route path="admin" element={<DashBoard />}>
              <Route
                path="product"
                element={(
                  <Suspense fallback={
                    <Box sx={{ display: 'flex' }}>
                      <CircularProgress />
                    </Box>}>
                    <ProductPage />
                  </Suspense>
                )}
              />
            </Route>
            <Route path="*" element={<NotFound />} />
          </Routes>
        </Suspense>
      </BrowserRouter>
    </>
  );
}

export default App;

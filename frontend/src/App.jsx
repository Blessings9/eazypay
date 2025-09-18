import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Payment from './components/Payment';
import Home from './components/Home'

function App() {

  return (

    <BrowserRouter>

      <div className="bg-blue-50 min-h-screen font-sans text-gray-800">

        <div>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/pay" element={<Payment />} />
          </Routes>
        </div>

      </div>

    </BrowserRouter>

  );
}

export default App;
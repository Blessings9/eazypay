import { useState } from 'react';
import axios from 'axios';
import { data, useNavigate } from "react-router-dom";
import Payment from './Payment';

const Home = () => {

    const [showModal, setShowModal] = useState(false);
    const [searchQuery, setSearchQuery] = useState('');
    const [searchResults, setSearchResults] = useState({

        date: '',
        cardNumber: '',
        status: '',
        message: '',
        transactionId: '',

    });

    const handleSearch = async (e) => {

        e.preventDefault();

        setSearchResults([]);

        const API_URL = `http://localhost:5221/api/eazypay/payment?transactionId=${searchQuery}`;

        try {

            const response = await axios.get(API_URL);

            if (response.data.code == 200) {

                setSearchResults(response.data.data);

            }

        } catch (error) {

            if (error.response.data.code == 404) {

                setSearchResults({

                    message: error.response.data.message,
                    date: '',
                    cardNumber: '',
                    status: '',
                    transactionId: ''

                });

            }

            if (error.response.data.code == 500) {

                setSearchResults({

                    message: error.response.data.message,
                    date: '',
                    cardNumber: '',
                    status: '',
                    transactionId: ''

                });

            } else {

                setSearchResults({

                    message: "Failed to fetch transactions. Please try again later.",
                    date: '',
                    cardNumber: '',
                    status: '',
                    transactionId: ''

                });

            }

        }

        setShowModal(true);

    };

    const closeModal = () => {

        setShowModal(false);
        setSearchResults(null);
        setSearchQuery('');

    };

    const dataExists = searchResults?.transactionId !== '' && searchResults?.cardNumber != ''

    return (

        <div className='min-h-screen flex items-center justify-center'>

            <div className='grid grid-cols-1 md:grid-cols-2 gap-4 max-w-5xl w-full'>

                <div className='flex flex-col min-h-[200px]'>
                    <Payment />
                </div>

                <div className='flex flex-col min-h-[300px]'>
                    <form onSubmit={handleSearch} className="mb-8 flex justify-center w-full">

                        <input
                            type="text"
                            placeholder="Enter Transaction ID..."
                            value={searchQuery}
                            onChange={(e) => setSearchQuery(e.target.value)}
                            className="bg-white w-full max-w-md p-3 border border-gray-300 rounded-l-lg shadow-sm focus:ring-indigo-500 focus:border-indigo-500 transition-all"
                            required
                        />

                        <button
                            type="submit"
                            onSubmit={handleSearch}
                            className="bg-indigo-600 hover:bg-indigo-700 text-white font-bold py-3 px-6 rounded-r-lg shadow-lg transform transition-all"
                        >
                            Search
                        </button>
                    </form>

                    {showModal && searchResults && (
                        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex justify-center items-center z-50">
                            <div className="bg-white p-8 rounded-lg shadow-xl max-w-md w-full mx-4 transform transition-all duration-300 scale-95 md:scale-100">
                                <h3 className="text-xl font-semibold mb-4 text-gray-800">{searchResults.message}</h3>
                                {
                                    dataExists && (
                                        <div className="space-y-4 text-left">

                                            <p className="text-gray-600">
                                                <span className="font-semibold text-gray-800">Date:</span> {searchResults.date}
                                            </p>
                                            <p className="text-gray-600">
                                                <span className="font-semibold text-gray-800">CardNumber:</span> {searchResults.cardNumber}
                                            </p>
                                            <p className="text-gray-600">
                                                <span className="font-semibold text-gray-800">Status:</span> {searchResults.status}
                                            </p>
                                            <p className="text-gray-600">
                                                <span className="font-semibold text-gray-800">Transaction ID:</span> {searchResults.transactionId}
                                            </p>
                                        </div>
                                    )
                                }

                                <button
                                    onClick={closeModal}
                                    className="mt-6 w-full bg-indigo-600 hover:bg-indigo-700 text-white font-bold py-2 px-4 rounded-md transition-colors"
                                >
                                    Close
                                </button>
                            </div>
                        </div>
                    )}
                </div>
            </div>

        </div>
    );
};

export default Home;
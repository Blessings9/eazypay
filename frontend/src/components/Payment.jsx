import { useState } from 'react';
import axios from 'axios';
import Modal from './Modal';

const Payment = () => {

    const [formData, setFormData] = useState({

        cardNumber: '',
        monthOfExpiry: '',
        yearOfExpiry: '',
        cvc: '',
        currencyCode: 'MWK',
        amount: 0,

    });

    const [errors, setErrors] = useState({});
    const [submissionStatus, setSubmissionStatus] = useState('');
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [paymentDetails, setPaymentDetails] = useState({

        date: '',
        cardNumber: '',
        message: '',
        status: '',
        transactionId: ''

    });

    const validate = () => {

        let newErrors = {};

        if (formData.cardNumber.replaceAll(" ", "").length != 16) {
            newErrors.cardNumber = 'Card number must be 16 digits.';
        }
        if (!formData.monthOfExpiry || !formData.yearOfExpiry) {
            newErrors.expiryDate = 'Expiry month and year are required.';
        }
        if (isNaN(formData.amount) || parseFloat(formData.amount) <= 0) {
            newErrors.amount = 'Amount must be a positive number.';
        }

        return newErrors;
    };

    const handleChange = (e) => {

        setErrors({});
        setSubmissionStatus('');

        const { name, value } = e.target;

        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const formatCardNumber = (value) => {

        const v = value.replace(/\s+/g, '').replace(/[^0-9]/gi, '');
        const matches = v.match(/\d{4,16}/g);
        const match = (matches && matches[0]) || '';
        const parts = [];
        for (let i = 0, len = match.length; i < len; i += 4) {
            parts.push(match.substring(i, i + 4));
        }
        if (parts.length) {
            return parts.join(' ');
        }

        return value;
    };

    const closeModal = () => {

        setErrors({});
        setSubmissionStatus('');
        setIsModalOpen(false)

    }

    const handleSubmit = async (e) => {

        e.preventDefault();

        const validationErrors = validate();

        if (Object.keys(validationErrors).length > 0) {

            setErrors(validationErrors);
            setSubmissionStatus('');

            return;
        }

        setErrors({});

        setSubmissionStatus('Processing ...');

        const API_URL_PAYMENT = 'http://eazypay-api-service:5001/api/eazypay/payment';

        try {

            const request = {

                cardNumber: formData.cardNumber.replace(/\s/g, ''),
                monthOfExpiry: formData.monthOfExpiry,
                yearOfExpiry: formData.yearOfExpiry,
                cvv: formData.cvc,
                currencyCode: formData.currencyCode,
                amount: formData.amount

            }

            console.log(request.amount)
            console.log(JSON.stringify(request))

            const response = await axios.post(API_URL_PAYMENT, request);

            if (response.data.code == 200) {

                setPaymentDetails({

                    message: response.data.message,
                    date: response.data.data.date,
                    cardNumber: response.data.data.cardNumber,
                    status: response.data.data.status,
                    transactionId: response.data.data.transactionId

                });

            }

            setIsModalOpen(true);
            console.log('Success:', response.data);

        } catch (error) {

            if (error.response.data.code == 422) {

                setPaymentDetails({

                    message: "Payment failed",
                    date: '',
                    cardNumber: '',
                    status: '',
                    transactionId: ''

                });

            }

            if (error.response.data.code == 400) {

                setPaymentDetails({

                    message: error.response.data.errors[1].details,
                    date: '',
                    cardNumber: '',
                    status: '',
                    transactionId: ''

                });

            }

            if (error.response.data.code === 500 == true) {

                setPaymentDetails({

                    message: error.response.data.message,
                    date: '',
                    cardNumber: '',
                    status: '',
                    transactionId: ''

                });

                console.error('Error code', paymentDetails.message)

            } else {

                setPaymentDetails({

                    message: 'Failed to make payment. Please try again later.',
                    date: '',
                    cardNumber: '',
                    status: '',
                    transactionId: ''

                });

            }

            setIsModalOpen(true);

        }
    };

    const months = Array.from({ length: 12 }, (_, i) => String(i + 1).padStart(2, '0'));
    const currentYear = new Date().getFullYear();
    const years = Array.from({ length: 10 }, (_, i) => String(currentYear + i));

    return (

        <div className="flex items-center justify-center">

            <Modal
                isOpen={isModalOpen}
                onClose={closeModal}
                date={paymentDetails.date}
                cardNumber={paymentDetails.cardNumber}
                message={paymentDetails.message}
                status={paymentDetails.status}
                transactionId={paymentDetails.transactionId}
            />
            <div className="w-full max-w-md p-8 space-y-6 bg-white rounded-lg shadow-md">
                {submissionStatus && (
                    <p className={`mt-4 text-center ${submissionStatus.includes('successful') ? 'text-green-600' : 'text-red-600'}`}>
                        {submissionStatus}
                    </p>
                )}
                <h2 className="text-2xl font-bold text-center text-gray-800">Payment Details</h2>
                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label htmlFor="cardNumber" className="block text-sm font-medium text-gray-700">Card Number</label>
                        <input
                            type="text"
                            name="cardNumber"
                            id="cardNumber"
                            value={formatCardNumber(formData.cardNumber)}
                            onChange={handleChange}
                            maxLength="19"
                            placeholder="e.g., 1234 5678 9101 1213"
                            className="mt-1 block w-full p-2 border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500"
                        />
                        {errors.cardNumber && <p className="mt-1 text-sm text-red-600">{errors.cardNumber}</p>}
                    </div>

                    <div>
                        <label htmlFor="cvc" className="block text-sm font-medium text-gray-700">CVC</label>
                        <input
                            type="text"
                            name="cvc"
                            id="cvc"
                            value={formatCardNumber(formData.cvc)}
                            onChange={handleChange}
                            maxLength="3"
                            placeholder="e.g. 234 173"
                            className="mt-1 block w-full p-2 border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500"
                        />
                    </div>

                    <div className="flex space-x-4">
                        <div className="flex-1">
                            <label htmlFor="monthOfExpiry" className="block text-sm font-medium text-gray-700">Expiry Month</label>
                            <select
                                name="monthOfExpiry"
                                id="monthOfExpiry"
                                value={formData.monthOfExpiry}
                                onChange={handleChange}
                                className="mt-1 block w-full p-2 border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500"
                            >
                                <option value="">Month</option>
                                {months.map(month => <option key={month} value={month}>{month}</option>)}
                            </select>
                        </div>
                        <div className="flex-1">
                            <label htmlFor="yearOfExpiry" className="block text-sm font-medium text-gray-700">Expiry Year</label>
                            <select
                                name="yearOfExpiry"
                                id="yearOfExpiry"
                                value={formData.yearOfExpiry}
                                onChange={handleChange}
                                className="mt-1 block w-full p-2 border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500"
                            >
                                <option value="">Year</option>
                                {years.map(year => <option key={year} value={year}>{year}</option>)}
                            </select>
                        </div>
                    </div>
                    {errors.expiryDate && <p className="mt-1 text-sm text-red-600">{errors.expiryDate}</p>}

                    <div>
                        <label htmlFor="currencyCode" className="block text-sm font-medium text-gray-700">Currency</label>
                        <select
                            name="currencyCode"
                            id="currencyCode"
                            value={formData.currencyCode}
                            onChange={handleChange}
                            className="mt-1 block w-full p-2 border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500"
                        >
                            <option value="MWK">MWK</option>
                            <option value="USD">USD</option>
                            <option value="ZAR">ZAR</option>
                            <option value="GPB">GPB</option>
                        </select>
                    </div>

                    <div>
                        <label htmlFor="amount" className="block text-sm font-medium text-gray-700">Amount</label>
                        <input
                            type="number"
                            name="amount"
                            id="amount"
                            value={formData.amount}
                            onChange={handleChange}
                            placeholder="e.g., 1000.00"
                            className="mt-1 block w-full p-2 border border-gray-300 rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500"
                        />
                        {errors.amount && <p className="mt-1 text-sm text-red-600">{errors.amount}</p>}
                    </div>

                    <button
                        type="submit"
                        className="w-full py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                    >
                        Submit Payment
                    </button>
                </form>
            </div>
        </div>

    );
};

export default Payment;
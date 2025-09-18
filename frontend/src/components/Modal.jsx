
const Modal = ({

    isOpen, onClose,
    date,
    cardNumber,
    message,
    status,
    transactionId,
}) => {

    if (!isOpen) return null;
    const dataExists = transactionId !== '' && cardNumber != '';

    return (
        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex justify-center items-center">
            <div className="relative p-8 bg-white w-96 max-w-full m-auto flex-col flex rounded-lg shadow-lg">
                <h3 className="text-xl font-semibold text-center mb-4 text-gray-800">{message}</h3>
                <p className="mb-2"></p>
                {
                    dataExists && (
                        <div className="space-y-2 text-gray-700">
                            <p><strong>Date:</strong>{date}</p>
                            <p><strong>Card Number:</strong>{cardNumber}</p>
                            <p><strong>Status:</strong>{status}</p>
                            <p><strong>Transaction ID:</strong> {transactionId}</p>
                        </div>
                    )
                }
                <div className="text-center mt-6">
                    <button
                        onClick={onClose}
                        className="py-2 px-4 bg-blue-900 hover:bg-green-600 text-white font-bold rounded-md"
                    >
                        Close
                    </button>
                </div>
            </div>
        </div>
    );
};

export default Modal;
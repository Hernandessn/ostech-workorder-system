import { Modal } from '../Modal'

export const DeleteWorkOrder = ({
    workOrder,
    isOpen,
    onClose,
    isSubmitting,
    onConfirm
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Delete workOrder">
            <p className="mb-4">
                Are you sure you want to delete <strong className="text-[#6EACDA]">{workOrder.title}</strong>?
            </p>
            <div className="flex justify-end gap-2">
                <button
                    onClick={onConfirm}
                    disabled={isSubmitting}
                    className="px-4 py-2 rounded-md bg-red-600 text-white hover:bg-red-700 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                >
                    {isSubmitting ? 'Deleting...' : 'Yes'}
                </button>
                <button
                    onClick={onClose}
                    className="px-4 py-2 rounded-md bg-[#6EACDA]/20 text-[#E2E2B6] hover:bg-[#6EACDA]/40 transition-colors"
                >
                    No
                </button>
            </div>
        </Modal>
    );
}